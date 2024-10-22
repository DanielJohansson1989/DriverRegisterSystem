using System.ComponentModel.DataAnnotations;

namespace DriverRegisterSystem.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Date and Time is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime NoteDate { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Description")]
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
