
using Bussines.Services;
using RabbitMQ.Core.Consumer;
using RabbitMQ.Core.Producer;

namespace RabbitMQ.WebApi
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
            builder.Services.AddScoped<IRabbitMQWrite, RabbitMQWrite>();
            builder.Services.AddScoped<IEmailService,EmailService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())//(Geliþtirme) ortamý olup olmadýðýný kontrol eder.
            {
                app.UseSwagger();// API'yi test etmek için kullanýlan araçtýr.
                app.UseSwaggerUI();//Swagger UI'sýný etkinleþtirir. Swagger UI, Swagger belgelerini görüntülemek ve API'yi test etmektir
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}