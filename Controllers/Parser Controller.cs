using Microsoft.AspNetCore.Mvc;
using Watcher.Services;

namespace Parser.Controllers
{
    [ApiController]
    [Route("/")]
    public class Controller : ControllerBase
    {
        private readonly HttpService _service;
        public Controller (HttpService service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<IActionResult> SendMessage()
        {
            string url = "http://localhost:5000/api/post";

            string message = "Hello, this is a message";

            await _service.SendMessageAsync(url, message);

            return Ok("Message sent successfully");
        }
    }
}