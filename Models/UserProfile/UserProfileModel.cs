namespace HET_BACKEND.Models.UserProfile
{
    public class UserProfileModel
    {
        public int userId {  get; set; }
        public string? FullName { get; set; }
        public string? Address {  get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? FamilyName { get; set; }
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly ModifiedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
