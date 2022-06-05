using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PortfolioAPI.Constraints;
using PortfolioAPI.Managers;
using PortfolioAPI.Managers.Interfaces;
using PortfolioAPI.Middleware;
using PortfolioAPI.Models;
using PortfolioAPI.Repository;
using PortfolioAPI.Repository.Interfaces;
using PortfolioAPI.SDK.Options;
using PortfolioAPI.SDK.Services;

namespace PortfolioAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<PortfolioDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddOptions<VantageAPIOptions>("VantageAPI")
                .Configure(o =>
                {
                    o.ServiceApiKey = (string)builder.Configuration.GetValue(typeof(string), "AlphaVantage:ServiceApiKey", "demo");
                });

            builder.Services.AddRouting(option =>
            {
                option.ConstraintMap["VantageVerbs"] = typeof(VantageVerbParameterTransformer);
                option.LowercaseUrls = true;
            });

            builder.Services.AddHttpClient<VantageAPIService>();

            // Add services to the container.
            builder.Services.TryAddTransient<IPortfolioRepository, PortfolioRepository>();
            builder.Services.TryAddTransient<ITradeRepository, TradeRepository>();

            builder.Services.TryAddTransient<IPortfolioManager, PortfolioManager>();
            builder.Services.TryAddTransient<ITradeManager, TradeManager>(); 

            builder.Services.AddControllers()
                .AddJsonOptions(j =>
                {
                    j.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}