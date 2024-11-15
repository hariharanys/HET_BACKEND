using HET_BACKEND.EntityModel;
using HET_BACKEND.Models.Expense;
using HET_BACKEND.ServiceModels.ExpenseDetails;
using Microsoft.EntityFrameworkCore;

namespace HET_BACKEND.Services.ExpenseServices
{
    public class ExpenseService:IExpenseService
    {
        private readonly HETDbContext _context;
        public ExpenseService(HETDbContext context)
        {
            _context = context;
        }

        //GetExpenseDetails
        public async Task<ExpenseDetailsServiceModel> GetExpenseDetails(ExpenseFilterModel expenseFilterModel)
        {
            try
            {
                //Fetching the query
                var query = _context.ExpenseDetails.AsQueryable();
                //Selecting the data based on UserId
                query = query.Where(x=>x.UserId == expenseFilterModel.UserId);
                //Search Text Filter
                if (!string.IsNullOrWhiteSpace(expenseFilterModel.searchText)) {
                    string searchPattern = $"%{expenseFilterModel.searchText}%";
                    query = query.Where(x => EF.Functions.Like(x.Title, searchPattern)
                    || EF.Functions.Like(x.Category, searchPattern)
                    || EF.Functions.Like(x.Notes, searchPattern));
                }
                //Getting Total Records
                var totalRecords = await query.CountAsync();
                //Selecting and assigning the Expense Records
                var ExpenseDetails = await query.OrderBy(x => x.Id)
                                          .Skip((expenseFilterModel.page - 1) * expenseFilterModel.pageSize)
                                          .Take(expenseFilterModel.pageSize)
                                          .Select(x => new GetExpenseDetailsModel
                                          {
                                              Id = x.Id,
                                              UserId = x.UserId,
                                              Title = x.Title,
                                              Category = x.Category,
                                              PaymentMethod = x.PaymentMehtod,
                                              Notes = x.Notes,
                                              Amount = x.Amount,
                                              IsRecurring = x.IsRecurring
                                          }).ToListAsync();
                return new ExpenseDetailsServiceModel
                {
                    totalRecords = totalRecords,
                    getExpenseDetailsModels = ExpenseDetails
                };

            }catch (Exception ex)
            {
                throw new Exception("An error occurred while performing [specific action].", ex);
            }
        }

        //Get Expense Based on expense Id
        public async Task<ExpenseEntityModel> GetExpenseDetailById(string expenseId)
        {
            try
            {
                var ExpenseDetail = await _context.ExpenseDetails.FirstOrDefaultAsync(x=>x.Id == Convert.ToInt32(expenseId));
                if(ExpenseDetail is null)
                {
                    throw new KeyNotFoundException("No Expenses have found");
                }
                return ExpenseDetail;
            }catch(Exception ex)
            {
                throw new Exception("An error occurred while performing [specific action].", ex);
            }
        }

        //Create Expense Details
        public async Task<ExpenseEntityModel> CreateExpenses(ExpenseModel expenseModel)
        {
            try
            {
                var expenses = new ExpenseEntityModel
                {
                    UserId = expenseModel.UserId,
                    Title = expenseModel.Title,
                    Category = expenseModel.Category,
                    PaymentMehtod = expenseModel.PaymentMethod,
                    Notes = expenseModel.Notes,
                    Amount = expenseModel.Amount,
                    IsRecurring = expenseModel.IsRecurring
                };
                _context.ExpenseDetails.Add(expenses);
                await _context.SaveChangesAsync();
                return expenses;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while performing [specific action].", ex);
            }
        }

        //Update Expense Details
        public async Task<ExpenseEntityModel> UpdateExpenses(ExpenseModel expenseModel)
        {
            try
            {
                //Finding existingExpense
                var existingExpense = await _context.ExpenseDetails.FirstOrDefaultAsync(x=>x.Id == expenseModel.Id);
                if(existingExpense is null)
                {
                    throw new KeyNotFoundException("No Expenses have found");
                }
                existingExpense.Title = expenseModel.Title;
                existingExpense.Category = expenseModel.Category;
                existingExpense.PaymentMehtod = expenseModel.PaymentMethod;
                existingExpense.Notes = expenseModel.Notes;
                existingExpense.Amount = expenseModel.Amount;
                existingExpense.IsRecurring = expenseModel.IsRecurring;
                _context.ExpenseDetails.Update(existingExpense);
                await _context.SaveChangesAsync();
                return existingExpense;
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while performing [specific action].", ex);
            }
        }
    }
}
