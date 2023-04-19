using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

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
