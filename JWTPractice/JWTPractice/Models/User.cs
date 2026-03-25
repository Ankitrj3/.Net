using System.ComponentModel.DataAnnotations;

namespace JWTPractice.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
