using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCrud.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Range(1,65)]
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }

    }
}
