namespace webapi.Repositories;

public interface IExpenseRepository
{
    Task<List<ExpenseDto>> GetExpensesAsync();
    Task<ExpenseDto> AddExpenseAsync(ExpenseDto expenseDto);
    Task<ExpenseDto> UpdateExpenseAsync(int id, ExpenseDto expense);
    Task DeleteExpenseAsync(int id);
}