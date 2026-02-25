using System.ComponentModel.DataAnnotations;

namespace SpendSmart.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public double Value { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Description must be 10 to 100 characters")]
        public string Description { get; set; }
    }
}
