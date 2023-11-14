using RabbitMQ.Core.Consumer;
namespace RabbitMQConsoleGmailSending
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RabbitMQRead rabbitMQRead = new RabbitMQRead();
            rabbitMQRead.Read();
        }
    }
}