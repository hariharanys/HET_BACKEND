using System.ComponentModel.DataAnnotations;
namespace HET_BACKEND.EntityModel
{
    public class UserEntityModel
    {
        [Key]
        public int userId { get; set; }
        [Required]
        public string? userName { get; set; }
        [Required]
        public string? password { get; set; }
        [Required]
        public string? email { get; set; }
        [Required]
        public string? phoneNumber { get; set; }
        [Required]
        public string? salt { get; set; }
        [Required]
        public DateOnly CreatedDate { get; set; }
        [Required]
        public DateOnly ModifiedDate { get; set; }
    }
}
