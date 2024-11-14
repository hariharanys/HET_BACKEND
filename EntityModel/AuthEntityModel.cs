using System.ComponentModel.DataAnnotations;

namespace HET_BACKEND.EntityModel
{
    public class AuthEntityModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber {  get; set; }
        [Required]
        public string Password { get; set; }

    }
}
