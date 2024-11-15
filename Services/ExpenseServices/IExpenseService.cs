using HET_BACKEND.EntityModel;
using HET_BACKEND.Models.Expense;
using HET_BACKEND.ServiceModels.ExpenseDetails;

namespace HET_BACKEND.Services.ExpenseServices
{
    public interface IExpenseService
    {
        Task<ExpenseDetailsServiceModel> GetExpenseDetails(ExpenseFilterModel expenseFilterModel);
        Task<ExpenseEntityModel> GetExpenseDetailById(string expenseId);
        Task<ExpenseEntityModel> CreateExpenses(ExpenseModel expenseModel);
        Task<ExpenseEntityModel> UpdateExpenses(ExpenseModel expenseModel);
    }
}
