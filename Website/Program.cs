using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PortfolioAPI.SDK.Options;
using PortfolioAPI.SDK.Services;
using Website.Data;
using Website.Managers;
using Website.Managers.Intefaces;

namespace Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddOptions<PortfolioAPIOptions>("PortfolioAPI")
                .Configure(o =>
                {
                    o.BaseAddress = builder.Configuration["PortfolioAPIUrl"];
                });

            builder.Services.AddHttpClient<PortfolioAPIService>();
            builder.Services.AddHttpClient<TradeAPIService>();
            builder.Services.AddHttpClient<VantageAPIService>();

            builder.Services.TryAddTransient<IPLManager, PLManager>();

            builder.Services.AddRazorPages();
            builder.Services.AddControllers()
                .AddJsonOptions(j =>
                {
                    j.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}