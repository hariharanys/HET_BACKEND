namespace HET_BACKEND.ServiceModels.ExpenseDetails
{
    public class ExpenseDetailsServiceModel
    {
        public long totalRecords { get; set; } = 0;
        public List<GetExpenseDetailsModel> getExpenseDetailsModels { get; set; } = new List<GetExpenseDetailsModel>();
    }
}
