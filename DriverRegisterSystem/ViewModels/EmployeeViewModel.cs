using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DriverRegisterSystem.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
