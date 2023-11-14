using Bussines.Services;
using Data.Entity;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Core.Consumer
{
    public class RabbitMQRead
    {
        private readonly IEmailService _service;
        public RabbitMQRead()
        {
            _service = new EmailService();
        }
        public void Read()
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(Environment.GetEnvironmentVariable("RABBITMQ_URI"))
            };

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            channel.QueueDeclare("NameQueue", true, false, false, null);
            //channel.BasicConsume(queue: "NameQueue", autoAck: false, consumer);//kuyrukta veri olmasa dahi çalışır
            channel.BasicQos(prefetchSize: 0, prefetchCount: 5, global: false);//prefetchSize kuyruk(byt) uzunluna göre verileri getirir,prefetchCount=verileri 5 beş alır
            Console.WriteLine(" [*] Waiting for messages.");
            consumer.Received += async (object sender, BasicDeliverEventArgs e) =>
            {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());
                    var email = JsonSerializer.Deserialize<EmailEntity>(message);
                    Console.WriteLine("Gelen Mesaj: " + message);
                    Console.ReadLine();
                    _service.Email(email);
                    channel.BasicAck(e.DeliveryTag, false);

            };
        }
    }
}
