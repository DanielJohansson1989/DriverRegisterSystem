using System.ComponentModel.DataAnnotations;

namespace DriverRegisterSystem.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        [Required]
        public DateTime NoteDate { get; set; }
        [Required]
        public string NoteDescription { get; set; }
        [Required]
        public string ResponsibleEmployee { get; set; }
        [Required]
        public decimal Income { get; set; }
        [Required]
        public decimal Expense { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
