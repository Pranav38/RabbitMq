using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMq.Common.Services;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMq.Producer.Conrtrollers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        
        public HomeController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        [HttpGet]
        public IActionResult Index()
         
        {
            using var connection = _rabbitMqService.CreateChannel();
            using var model = connection.CreateModel();
            model.QueueDeclare("DataPipeline", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var myBatchDetails = new { BatchId = "3" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(myBatchDetails));
            model.BasicPublish("DataPipeline",
                                 string.Empty,
                                 basicProperties: null,
                                 body: body);

            return Ok();
        }
    }
}
