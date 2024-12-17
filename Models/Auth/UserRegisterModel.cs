namespace HET_BACKEND.Models.Auth
{
    public class UserRegisterModel
    {
        public string? userName {  get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? salt { get; set; }
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly ModifiedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
