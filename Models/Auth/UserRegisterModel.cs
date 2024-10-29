namespace HET_BACKEND.Models.Auth
{
    public class UserRegisterModel
    {
        public string? userName {  get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? salt { get; set; }
    }
}
