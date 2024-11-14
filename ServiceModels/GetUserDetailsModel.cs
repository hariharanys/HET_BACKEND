namespace HET_BACKEND.ServiceModels
{
    public class GetUserDetailsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public string FullName {  get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty; 
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string FamilyName { get; set; } = string.Empty;

    }
}
