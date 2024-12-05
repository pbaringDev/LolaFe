
using LolaFe.API.Services;
using Serilog;

namespace LolaFe.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/lolafe.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


            var builder = WebApplication.CreateBuilder(args);

            //third party logger
            builder.Services.AddSerilog();

            ////default logger
            //builder.Logging.ClearProviders();
            //builder.Logging.AddConsole();

            // Add services to the container.

            builder.Services.AddControllers(options => 
            { 
                //options.ReturnHttpNotAcceptable = true;

            }).AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IMailService, LocalMailService>();
            builder.Services.AddScoped<BookingDataStore>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
