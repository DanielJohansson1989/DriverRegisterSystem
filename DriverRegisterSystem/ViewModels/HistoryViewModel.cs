using DriverRegisterSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DriverRegisterSystem.ViewModels
{
    public class HistoryViewModel
    {
        public IEnumerable<Note> Notes { get; set; }
        public IEnumerable<SelectListItem> SelectableDrivers { get; set; } = null;
        public IEnumerable<SelectListItem> SelectableEmployees { get; set; } = null;
        public string DriverFilter { get; set; }
        public string EmployeeFilter { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.AddYears(-5);
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
