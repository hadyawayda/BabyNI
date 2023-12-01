using Aggregator.Aggregators;
using Aggregator.Connection;
using Aggregator.Factory;
using Aggregator.Services;
using Vertica.Data.VerticaClient;

namespace Aggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string? connectionString = builder.Configuration.GetConnectionString("Vertica");

            // Add services to the container.
            builder.Services.AddSingleton(provider => new VerticaConnection(connectionString));
            builder.Services.AddSingleton<IDbConnection, DbConnection>();
            builder.Services.AddSingleton<IAggregator, DbAggregator>();
            builder.Services.AddSingleton<IAggregatorFactory, AggregatorFactory>();
            builder.Services.AddHostedService<AggregatorService>();
            builder.Services.AddHttpClient();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}