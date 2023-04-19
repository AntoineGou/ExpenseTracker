using NUnit.Framework;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TechTalk.SpecFlow.Assist;
using webapi.Models;

namespace tests.StepDefinitions;

[Binding]
public class ExpenseControllerStepDefinitions
{
    private readonly BackendContext _context;

    public ExpenseControllerStepDefinitions(BackendContext context)
    {
        _context = context;
    }
    [Given(@"a webapi")]
    public void GivenAWebapi()
    {
        _context.ClearDb();
    }

    [When(@"I GET all Expenses")]
    public async Task WhenIgetAllExpenses()
    {
        _context.LastResult = await _context.Api.CreateClient().GetAsync($"api/Expenses/");
    }

    [When(@"I POST an expense")]
    public async Task WhenIpostAnExpense(Table table)
    {
        var expenseDto = table.CreateInstance<ExpenseDto>();
        _context.LastResult = await _context.Api.CreateClient().PostAsJsonAsync("api/Expenses/", expenseDto);
    }

    [Then(@"status code is (.*)")]
    public void ThenStatusCodeIs(int code)
    {
        Assert.AreEqual(code, (int)_context.LastResult.StatusCode);
    }

    [Then(@"Expenses are")]
    public async Task ThenExpensesAre(Table table)
    {
        var expenses = await TestHelper.Deserialize<IEnumerable<ExpenseDto>>(_context.LastResult);
        table.CompareToSet(expenses);
    }

    [When(@"I PUT Expense (.*) to")]
    public async Task WhenIputExpenseTo(int id, Table table)
    {
        var expenseDto = table.CreateInstance<ExpenseDto>();
        _context.LastResult = await _context.Api.CreateClient().PutAsJsonAsync($"api/Expenses/{id}", expenseDto);

    }

    [When(@"I DELETE Expense (.*)")]
    public async Task WhenIdeleteExpense(int id)
    {
        _context.LastResult = await _context.Api.CreateClient().DeleteAsync($"api/Expenses/{id}");
    }
}

public static class TestHelper
{
    private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };
    public static async Task<T> Deserialize<T>(HttpResponseMessage response)
        => await JsonSerializer.DeserializeAsync<T>(new MemoryStream(await response.Content.ReadAsByteArrayAsync()), SerializerOptions);

}