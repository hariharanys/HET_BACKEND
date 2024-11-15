namespace HET_BACKEND.Models.Expense
{
    public class ExpenseFilterModel
    {
        public int UserId { get; set; } = 0;
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string searchText { get; set; } = string.Empty;
        public string startDate { get; set; } = string.Empty;
        public string endDate { get; set; } = string.Empty;
        public bool asSorting { get; set; }
        public bool descSorting { get; set; }
        public bool isRecurring { get; set; }

    }
}
