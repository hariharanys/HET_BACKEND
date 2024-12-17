using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HET_BACKEND.EntityModel
{
    public class ExpenseEntityModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public UserEntityModel? User { get; set; }
        [Required]
        public string Title { get; set; }
        public string Category { get; set; } = string.Empty;
        public string PaymentMehtod { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        [Required]
        public long Amount { get; set; }
        [Required]
        public bool IsRecurring { get; set; }
        [Required]
        public DateOnly CreatedDate { get; set; }
        [Required]
        public DateOnly ModifiedDate { get; set; }
    }
}
