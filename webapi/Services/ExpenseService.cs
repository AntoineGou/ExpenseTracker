using webapi.Models;
using webapi.Repositories;

namespace webapi.Services;

public class ExpenseService
{
    private readonly IExpenseRepository _repository;

    public ExpenseService(IExpenseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ExpenseDto>> GetExpensesAsync()
    {
        return await _repository.GetExpensesAsync();
    }

    public async Task<ExpenseDto> AddExpenseAsync(ExpenseDto expenseDto)
    {
        return await _repository.AddExpenseAsync(expenseDto);
    }

    public async Task<ExpenseDto> UpdateExpenseAsync(int id, ExpenseDto expense)
    {
        return await _repository.UpdateExpenseAsync(id, expense);
    }

    public async Task DeleteExpenseAsync(int id)
    {
        await _repository.DeleteExpenseAsync(id);
    }
}