using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HET_BACKEND.EntityModel
{
    public class UserDetailsEntityModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public UserEntityModel? User { get; set; }
        public string? FullName { get; set; }
        
        public string? Address {  get; set; }
        public string? City {  get; set; }
        public string? State { get; set; }
        public string? Country { get; set;}
        public string? PostalCode { get; set; } 
        public string? FamilyName { get; set; }

    }
}
