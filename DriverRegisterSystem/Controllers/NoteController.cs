using DriverRegisterSystem.Models;
using DriverRegisterSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriverRegisterSystem.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        public async Task<IActionResult> AddNote(int id)
        {
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Note note, int id)
        {
            if (ModelState.IsValid)
            {
                await _noteRepository.Add(note);
                return RedirectToAction("DriverInfo", "Driver", new { id = id });
            }
            return View(note);
        }
    }
}
