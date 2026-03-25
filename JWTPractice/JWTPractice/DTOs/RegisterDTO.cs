using System.ComponentModel.DataAnnotations;

namespace JWTPractice.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string name { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
