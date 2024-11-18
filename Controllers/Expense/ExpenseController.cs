using HET_BACKEND.Models.Expense;
using HET_BACKEND.Services.ExpenseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HET_BACKEND.Controllers.Expense
{
    [Route("HET/[controller]/[action]")]
    public class ExpenseController : Controller
    {
       private readonly IExpenseService _expenseService;
       public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> GetExpenseDetails([FromBody] ExpenseFilterModel expenseFilterModel)
        {
            if(expenseFilterModel .UserId== 0) {
                throw new ArgumentException("Enter Valid User Id");
            }
            var expenseList = await _expenseService.GetExpenseDetails(expenseFilterModel);
            return Json(new { Result = "SUCCESS", Message = expenseList });
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetExpenseDetailById([FromQuery]string expenseId)
        {
            var expenseDetail = await _expenseService.GetExpenseDetailById(expenseId);
            return Json(new { Result = "SUCCESS", Message = expenseDetail });
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> CreateExpenses([FromBody] ExpenseModel expense)
        {
            var expenseDetail = await _expenseService.CreateExpenses(expense);
            return Json(new { Result = "SUCCESS", Message = expenseDetail });
        }

        [HttpPut]
        [Authorize]
        public async Task<JsonResult> UpdateExpenses([FromBody] ExpenseModel expense)
        {
            var expenseDetail =await  _expenseService.UpdateExpenses(expense);
            return Json(new {Result="Updated Successfully",Message= expenseDetail });
        }
    }
}
