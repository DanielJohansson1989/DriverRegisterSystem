using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DriverRegisterSystem.ViewModels
{
    public class EmployeeViewModel : IValidatableObject
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

        public bool changePasswordCheckbox { get; set; } = false;
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (changePasswordCheckbox)
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    validationResults.Add(new ValidationResult("Password is required when changing password.", new[] { "Password" }));
                }

                if (string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    validationResults.Add(new ValidationResult("Confirm password is required when changing password.", new[] { "ConfirmPassword" }));
                }
            }

            return validationResults;
        }
    }
}
