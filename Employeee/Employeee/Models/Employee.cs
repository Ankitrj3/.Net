using System.ComponentModel.DataAnnotations;

namespace Employeee.Models
{
    public class Employee
    {
        [Display(Name = "Employee ID")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? City { get; set; }
    }
}
