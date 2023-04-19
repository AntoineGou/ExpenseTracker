using System.Text.Json;
using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;

namespace webapi.Models;

public class ExpenseDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Recipient { get; set; }
    public string Currency { get; set; }
    [JsonConverter(typeof(ExpenseTypeJsonConverter))]
    public ExpenseType Type { get; set; }

}

public enum ExpenseType
{
    Other,
    Food,
    Drinks
}

public class ExpenseTypeJsonConverter : JsonConverter<ExpenseType>
{
    public override ExpenseType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();
        return Enum.TryParse<ExpenseType>(stringValue, true, out var result) ? result : ExpenseType.Other;
    }

    public override void Write(Utf8JsonWriter writer, ExpenseType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

[Table("Expense")]
public class Expense
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Recipient { get; set; }
    public string Currency { get; set; }
    public ExpenseType Type { get; set; }
}

public static class ExpenseExtensions
{
    public static ExpenseDto ToDto(this Expense expense)
    {
        return new ExpenseDto
        {
            Id = expense.Id,
            Date = expense.Date,
            Amount = expense.Amount,
            Recipient = expense.Recipient,
            Currency = expense.Currency,
            Type = expense.Type
        };
    }

    public static Expense FromDto(this ExpenseDto expenseDto)
    {
        return new Expense
        {
            Id = expenseDto.Id,
            Date = expenseDto.Date,
            Amount = expenseDto.Amount,
            Recipient = expenseDto.Recipient,
            Currency = expenseDto.Currency,
            Type = expenseDto.Type
        };
    }
}
