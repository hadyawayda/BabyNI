using Loader.Connection;
using Loader.Factory;
using Loader.Loaders;
using Vertica.Data.VerticaClient;

namespace Loader
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
            builder.Services.AddScoped<IDbInitalizer, DbInitializer>();
            builder.Services.AddScoped<ILoaderFactory, LoaderFactory>();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IDbInitalizer>();
            }

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