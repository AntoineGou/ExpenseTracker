using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpensesController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseDto>>> GetExpenses()
        {
            var expenses = await _expenseService.GetExpensesAsync();
            return Ok(expenses);
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseDto>> AddExpense([FromBody] ExpenseDto expenseDto)
        {
            var addedExpense = await _expenseService.AddExpenseAsync(expenseDto);
            return CreatedAtAction(nameof(GetExpenses), new { id = addedExpense.Id }, addedExpense);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExpenseDto>> UpdateExpense(int id, [FromBody] ExpenseDto expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedExpense = await _expenseService.UpdateExpenseAsync(id, expense);
                return Ok(updatedExpense);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ExpenseDto>> DeleteExpense(int id)
        {
            try
            {
                await _expenseService.DeleteExpenseAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}


public enum ExpenseType
    {
        Other,
        Food,
        Drinks
    }
