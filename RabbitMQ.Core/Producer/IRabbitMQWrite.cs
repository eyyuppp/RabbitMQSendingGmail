using Data.Entity;

namespace RabbitMQ.Core.Producer
{
    public interface IRabbitMQWrite
    {
        void EmailWrite(EmailEntity email);
    }
}
