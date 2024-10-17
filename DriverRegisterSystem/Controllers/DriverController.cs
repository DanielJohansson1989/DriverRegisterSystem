using DriverRegisterSystem.Models;
using DriverRegisterSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriverRegisterSystem.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriverRegisterRepository<Driver> _driverRepository;
        public DriverController(IDriverRegisterRepository<Driver> driverRepository)
        {
            _driverRepository = driverRepository;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var result = await _driverRepository.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(d => d.DriverName.ToLower().Contains(searchString.ToLower()));
            }
            return View(result);
        }

        public async Task<IActionResult> DriverInfo(int id, DateTime? startDate, DateTime? endDate)
        {
            ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");

            var result = await _driverRepository.GetById(id);
            if (result == null)
            {
                return NotFound(result);
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                result.Notes = result.Notes.Where(n => n.NoteDate >= startDate.Value && n.NoteDate <= endDate.Value);
            }
            return View(result);
        }

        public async Task<IActionResult> DeleteDriver(int id)
        {
            var result = await _driverRepository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            await _driverRepository.Delete(result);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddDriver()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDriver(Models.Driver driver)
        {
            if (ModelState.IsValid)
            {
                await _driverRepository.Add(driver);
                return RedirectToAction("Index");
            }
            return View(driver);
        }

        public async Task<IActionResult> EditDriver(int id)
        {
            var result = await _driverRepository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditDriver(Models.Driver driver)
        {
            if (ModelState.IsValid)
            {
                await _driverRepository.Update(driver);
                return RedirectToAction("Index");
            }
            return View(driver);
        }
    }
}
