using DriverRegisterSystem.Models;
using DriverRegisterSystem.Services;
using DriverRegisterSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DriverRegisterSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<Employee> _signInManager;
        private readonly INoteRepository _noteRepository;
        private readonly IDriverRegisterRepository<Driver> _driverRepository;

        public HomeController(ILogger<HomeController> logger, SignInManager<Employee> signInManager, INoteRepository noteRepository, IDriverRegisterRepository<Driver> driverRepository)
        {
            _logger = logger;
            _signInManager = signInManager;
            _noteRepository = noteRepository;
            _driverRepository = driverRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var allNotes = await _noteRepository.GetAll();
                if (allNotes == null)
                {
                    return NotFound();
                }
                IEnumerable<Note> recentNotes;
                
                if (User.IsInRole("Admin"))
                {
                    recentNotes = allNotes.Where(n => n.NoteDate >= DateTime.Now.AddHours(-24) && n.NoteDate <= DateTime.Now).ToList();
                    return View(recentNotes);
                }
                else
                {
                    recentNotes = allNotes.Where(n => n.NoteDate >= DateTime.Now.AddHours(-12) && n.NoteDate <= DateTime.Now).ToList();
                    return View(recentNotes);
                }
            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> History(HistoryViewModel model)
        {
            var result = await _noteRepository.GetAll();
            if (result == null)
            {
                return NotFound();
            }

            //Fetch Drivers and Users for dropdown
            var drivers = (await _driverRepository.GetAll()).Select(d => new SelectListItem
            {
                Value = d.DriverName,
                Text = d.DriverName
            }).ToList();

            var employees = (await _signInManager.UserManager.Users.ToListAsync()).Select(e => new SelectListItem
            {
                Value = e.Name,
                Text = e.Name
            }).ToList();

            model.SelectableEmployees = employees;
            model.SelectableDrivers = drivers;


            // Filter notes
            if (model.EmployeeFilter != null)
            {
                result = result.Where(n => n.ResponsibleEmployee == model.EmployeeFilter).ToList();
            }
            if (model.DriverFilter != null)
            {
                result = result.Where(n => n.Driver.DriverName == model.DriverFilter).ToList();
            }

            model.Notes = result.Where(n => n.NoteDate >= model.StartDate && n.NoteDate <= model.EndDate)
                .OrderByDescending(n => n.NoteDate).ToList();

            // Calculate total income and expense for the company
            // If result is filtered before this it effects the total income and expense
            decimal companyTotalIncome = result.Select(n => n.Income).Sum();
            decimal companyTotalExpense = result.Select(n => n.Expense).Sum();
            ViewBag.CompanyTotalIncome = companyTotalIncome;
            ViewBag.CompanyTotalExpense = companyTotalExpense;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
