using Microsoft.AspNetCore.Mvc.Testing;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using webapi;

namespace tests.StepDefinitions
{
    public class BackendContext
    {
        public WebApplicationFactory<Startup> Api { get; }
        public HttpResponseMessage LastResult { get; set; }

        protected const string CONNECTION_STRING = "Server=127.0.0.1;Database=model;User Id=SA;Password=azerty123!;Encrypt=False;TrustServerCertificate=True;";
        public BackendContext()
        {
            Api = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(
                    builder => builder
                        .ConfigureAppConfiguration((_, b) =>
                        {
                           b.AddInMemoryCollection(new List<KeyValuePair<string, string>>
                            {
                            new ("Sql:ConnectionString", CONNECTION_STRING)
                            });
                        })
                        );
        }

        public async Task ClearDb()
        {
            await using var connection = new SqlConnection(CONNECTION_STRING);
            await connection.ExecuteAsync("TRUNCATE TABLE Expense");
        }
    }
}
