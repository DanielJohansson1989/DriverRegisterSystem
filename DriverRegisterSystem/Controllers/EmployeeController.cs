using DriverRegisterSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DriverRegisterSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        public EmployeeController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var result = await _userManager.GetUsersInRoleAsync("Employee");
            if (result == null)
            {
                return NotFound();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(u => u.UserName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(result);
        }

        public IActionResult Create()
        {
            return RedirectToPage("/Account/Register", new { area = "Identity" });
        }
    }
}
