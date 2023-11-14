
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
            if (app.Environment.IsDevelopment())//(Geli�tirme) ortam� olup olmad���n� kontrol eder.
            {
                app.UseSwagger();// API'yi test etmek i�in kullan�lan ara�t�r.
                app.UseSwaggerUI();//Swagger UI's�n� etkinle�tirir. Swagger UI, Swagger belgelerini g�r�nt�lemek ve API'yi test etmektir
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}