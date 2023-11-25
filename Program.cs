using BabyAPI.Connection;
using System.Configuration;
using Vertica.Data.VerticaClient;

namespace BabyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string? connectionString = builder.Configuration.GetConnectionString("Vertica");

            // Add services to the container.

            builder.Services.AddScoped(provider => new VerticaConnection(connectionString));
            builder.Services.AddScoped<IDbConnection, DbConnection>();
            builder.Services.AddControllers();
            builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod()));

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

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}