
using STINProject_API.Services.ExchangeRateService;
using STINProject_API.Services.PersistenceService;
using STINProject_API.Services.TransactionService;

namespace STINProject_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SQLiteDataContext>();
            builder.Services.AddScoped<IPersistenceService, SQLitePersistenceService>();

            builder.Services.AddSingleton<IExchangeRateService, SimpleExchangeRateService>();
            builder.Services.AddSingleton<IExchangeRateGetter, ExchangeRateGetter>();

            builder.Services.AddSingleton<ITransactionService, SimpleTransactionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}