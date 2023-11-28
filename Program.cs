using Parser.Controllers;
using Parser.Factory;
using Parser.Watcher;
using Parser.Watcher_Factory;
using Watcher.Services;

namespace Parser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
            //builder.Services.AddSingleton<Controller>();
            builder.Services.AddSingleton<HttpService>();
            builder.Services.AddHostedService<FileWatcher>();
            builder.Services.AddSingleton<IParserFactory, ParserFactory>();
            builder.Services.AddSingleton<IWatcherFactory, WatcherFactory>();

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