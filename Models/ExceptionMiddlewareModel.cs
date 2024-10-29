namespace HET_BACKEND.Models
{
    public class ExceptionMiddlewareModel
    {
        public int? statusCode { get; set; }
        public string? message { get; set; }
        public string? messageTree { get; set; }
    }
}
