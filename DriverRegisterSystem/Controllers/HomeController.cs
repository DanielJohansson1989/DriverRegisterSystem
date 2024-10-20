using DriverRegisterSystem.Models;
using DriverRegisterSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DriverRegisterSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<Employee> _signInManager;
        private readonly INoteRepository _noteRepository;

        public HomeController(ILogger<HomeController> logger, SignInManager<Employee> signInManager, INoteRepository noteRepository)
        {
            _logger = logger;
            _signInManager = signInManager;
            _noteRepository = noteRepository;
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
                var recentNotes = allNotes.Where(n => n.NoteDate >= DateTime.Now.AddHours(-24) && n.NoteDate <= DateTime.Now).ToList();
                return View(recentNotes);
            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
