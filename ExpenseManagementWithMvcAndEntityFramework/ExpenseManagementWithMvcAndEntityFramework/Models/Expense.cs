using System.ComponentModel.DataAnnotations;

namespace ExpenseManagementWithMvcAndEntityFramework.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
