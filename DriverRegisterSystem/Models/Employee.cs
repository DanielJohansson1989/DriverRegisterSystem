using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DriverRegisterSystem.Models
{
    public class Employee : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
