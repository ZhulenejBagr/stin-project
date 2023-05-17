using Google.Authenticator;
using Hangfire.AspNetCore;
using Microsoft.AspNetCore.ResponseCompression;
using STINProject.Server.Services.ExchangeRateService;
using STINProject.Server.Services.LoginService;
using STINProject.Server.Services.PersistenceService;
using STINProject.Server.Services.TransactionService;

namespace STINProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddRazorPages();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<SQLiteDataContext>();
            builder.Services.AddScoped<IPersistenceService, SQLitePersistenceService>();

            builder.Services.AddScoped<IExchangeRateService, SimpleExchangeRateService>();
            builder.Services.AddScoped<IExchangeRateGetter, ExchangeRateGetter>();

            builder.Services.AddScoped<ITransactionService, SimpleTransactionService>();

            builder.Services.AddTransient<ILoginService, SimpleLoginService>();
            builder.Services.AddSingleton<SessionStorage>();
            builder.Services.AddScoped<TwoFactorService>();
            builder.Services.AddScoped<TwoFactorAuthenticator>();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            builder.Services.AddSingleton(configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}