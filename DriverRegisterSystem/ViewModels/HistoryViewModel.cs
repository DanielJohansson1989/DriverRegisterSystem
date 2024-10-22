using DriverRegisterSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DriverRegisterSystem.ViewModels
{
    public class HistoryViewModel
    {
        public IEnumerable<Note> Notes { get; set; }
        public IEnumerable<SelectListItem> SelectableDrivers { get; set; } = null;
        public IEnumerable<SelectListItem> SelectableEmployees { get; set; } = null;
        public string DriverFilter { get; set; }
        public string EmployeeFilter { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } = DateTime.Now.AddYears(-5);
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
