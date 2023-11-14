using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Core.Producer;

namespace RabbitMQ.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RabbitMQGmailSendingController : ControllerBase
    {
        readonly private IRabbitMQWrite _rabbitMQWrite;
        public RabbitMQGmailSendingController(IRabbitMQWrite rabbitMQWrite)
        {
            _rabbitMQWrite = rabbitMQWrite;
        }

        [HttpPost]
        public IActionResult Post(EmailEntity email)
        {
            _rabbitMQWrite.EmailWrite(email);
            return StatusCode(201);
        }
    }
}
