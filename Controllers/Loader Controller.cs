using Microsoft.AspNetCore.Mvc;

namespace Loader.Controllers
{
    public class MessageModel
    {
        public string? Message { get; set; }
    }

    [ApiController]
    [Route("api")]
    public class ParserController : ControllerBase
    {
        [HttpPost("post")]
        public IActionResult ReceiveMessage([FromBody] MessageModel message)
        {
            Console.WriteLine(message.Message);

            return Ok("Message received and processed");
        }
    }
}