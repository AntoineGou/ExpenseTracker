using Microsoft.AspNetCore.Mvc.Testing;
using webapi;

namespace tests.StepDefinitions
{
    public class BackendContext
    {
        public WebApplicationFactory<Startup> Api { get; }
        public HttpResponseMessage LastResult { get; set; }

        public BackendContext()
        {
            Api = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(
                    builder => builder
                        .ConfigureAppConfiguration((_, b) =>
                        {
                            //todo conf
                        })
                        );
        }
    }
}
