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
            AddNoteViewModel model = new AddNoteViewModel
            {
                DriverId = driverId,
                ResponsibleEmployee = currentUser.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Note note, int driverId)
        {
            if (ModelState.IsValid)
            {
                await _noteRepository.Add(note);
                return RedirectToAction("DriverInfo", "Driver", new { id = driverId });
            }
            return View(note);
        }
    }
}
