using DriverRegisterSystem.Models;
using DriverRegisterSystem.Services;
using DriverRegisterSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverRegisterSystem.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        public async Task<IActionResult> AddNote(int driverId)
        {
            AddNoteViewModel model = new AddNoteViewModel
            {
                DriverId = driverId,
                ResponsibleEmployee = User.Identity.Name
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
