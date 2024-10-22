using DriverRegisterSystem.Models;
using DriverRegisterSystem.Services;
using DriverRegisterSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DriverRegisterSystem.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly UserManager<Employee> _userManager;
        public NoteController(INoteRepository noteRepository, UserManager<Employee> userManager)
        {
            _noteRepository = noteRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> AddNote(int driverId)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            Note model = new Note
            {
                DriverId = driverId,
                ResponsibleEmployee = currentUser.Name,
                NoteDate = DateTime.Now
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Note model)
        {
            if (ModelState.IsValid)
            {
                await _noteRepository.Add(model);
                return RedirectToAction("DriverInfo", "Driver", new { id = model.DriverId });
            }
            return View(model);
        }
    }
}
