using CommonLib.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PortfolioAPI.Constraints;
using PortfolioAPI.Middleware;
using PortfolioAPI.SDK.Options;
using PortfolioAPI.SDK.Services;

namespace PortfolioAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddControllers();
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