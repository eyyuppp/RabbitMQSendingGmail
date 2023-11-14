using Data.Entity;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Core.Producer
{
    public class RabbitMQWrite : IRabbitMQWrite
    {
        public void EmailWrite(EmailEntity email)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                Uri = new Uri(Environment.GetEnvironmentVariable("RABBITMQ_URI"))
            };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "NameQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(email));
                channel.BasicPublish(exchange: "", routingKey: "NameQueue", body: body);
                Console.WriteLine(email.Subject + " mesaj gönderildi.");
            }
        }
    }
}
