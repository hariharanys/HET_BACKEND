namespace HET_BACKEND.Models.Expense
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public long Amount { get; set; }
        public bool IsRecurring { get; set; }
    }
}
