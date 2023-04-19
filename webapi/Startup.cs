using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using webapi.Repositories;
using webapi.Services;

namespace webapi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

       services.AddSingleton(_configuration.GetSection("Sql").Get<SqlConfig>());
        services.AddSingleton<IExpenseRepository, SqlExpenseRepository>();
        services.AddTransient<ExpenseService>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}