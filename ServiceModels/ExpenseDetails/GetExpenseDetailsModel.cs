namespace HET_BACKEND.ServiceModels.ExpenseDetails
{
    public class GetExpenseDetailsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public long Amount { get; set; } = 0;
        public bool IsRecurring { get; set; } = false;
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly ModifiedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
