using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using webapi.Models;

namespace webapi.Repositories
{
    public class SqlExpenseRepository : IExpenseRepository
    {
        private readonly SqlConfig _config;

        public SqlExpenseRepository(SqlConfig config)
        {
            _config = config;
        }

        public async Task<List<ExpenseDto>> GetExpensesAsync()
        {
            await using var connection = new SqlConnection(_config.ConnectionString);
            var expenses = await connection.GetAllAsync<Expense>();
            return expenses.Select(e => e.ToDto()).ToList();
        }

        public async Task<ExpenseDto> AddExpenseAsync(ExpenseDto expenseDto)
        {
            var expense = expenseDto.FromDto();
            await using var connection = new SqlConnection(_config.ConnectionString);
            var id = await connection.InsertAsync(expense);
            expense.Id = id;
            return expense.ToDto();
        }

        public async Task<ExpenseDto> UpdateExpenseAsync(int id, ExpenseDto expenseDto)
        {
            var expense = expenseDto.FromDto();
            expense.Id = id;
            await using var connection = new SqlConnection(_config.ConnectionString);
            await connection.UpdateAsync(expense);
            return expense.ToDto();
        }

        public async Task DeleteExpenseAsync(int id)
        {
            await using var connection = new SqlConnection(_config.ConnectionString);
            var expense = await connection.GetAsync<Expense>(id);
            if (expense != null)
            {
                await connection.DeleteAsync(expense);
            }
        }
    }
}

public class SqlConfig
{
    public string ConnectionString { get; set; }
}