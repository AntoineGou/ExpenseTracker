using System.Text.Json.Serialization;
using System.Text.Json;

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
