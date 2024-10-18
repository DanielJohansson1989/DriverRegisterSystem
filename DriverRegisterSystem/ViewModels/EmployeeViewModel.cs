using Microsoft.AspNetCore.Mvc.Rendering;

namespace DriverRegisterSystem.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
