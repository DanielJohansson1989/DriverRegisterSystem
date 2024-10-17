using System.ComponentModel.DataAnnotations;

namespace DriverRegisterSystem.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        [Required]
        public string DriverName { get; set; }
        [Required]
        [MaxLength(6)]
        public string CarReg { get; set; }
        public IEnumerable<Note> Notes { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalIncome { get; set; }

    }
}
