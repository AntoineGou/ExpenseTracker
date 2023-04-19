using webapi.Models;

namespace webapi.Repositories;

public class InMemoryExpenseRepository : IExpenseRepository
{
    private readonly Dictionary<int, ExpenseDto> _expenses = new ();

    public async Task<List<ExpenseDto>> GetExpensesAsync()
    {
        return await Task.FromResult(_expenses.Values.ToList());
    }

    public async Task<ExpenseDto> AddExpenseAsync(ExpenseDto expenseDto)
    {
        expenseDto.Id = _expenses.Any() ? _expenses.Keys.Max() + 1 : 1;
        _expenses[expenseDto.Id] = expenseDto;
        return await Task.FromResult(expenseDto);
    }

    public async Task<ExpenseDto> UpdateExpenseAsync(int id, ExpenseDto expense)
    {
        _expenses[id] = expense;
        return await Task.FromResult(expense);
    }

    public Task DeleteExpenseAsync(int id)
    {
        _expenses.Remove(id);
        return Task.CompletedTask;
    }
}