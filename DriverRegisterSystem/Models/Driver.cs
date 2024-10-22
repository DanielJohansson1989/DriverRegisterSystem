using System.ComponentModel.DataAnnotations;

namespace DriverRegisterSystem.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string DriverName { get; set; }
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Only alphanumeric characters are allowed.")]
        [Display(Name = "Car Registration Number")]
        public string CarReg { get; set; }
        public IEnumerable<Note> Notes { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalIncome { get; set; }

    }
}
