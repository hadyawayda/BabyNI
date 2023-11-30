using Loader.Factory;
using Microsoft.AspNetCore.Mvc;

namespace Loader.Controllers
{
    [ApiController]
    [Route("api")]
    public class ParserController : ControllerBase
    {
        private readonly ILoaderFactory _loaderFactory;

        public ParserController(ILoaderFactory loaderFactory)
        {
            _loaderFactory = loaderFactory;
        }

        [HttpPost("post")]
        public IActionResult ReceiveMessage([FromBody] MessageModel message)
        {
            _loaderFactory.CreateLoader(message.Message!);

            return Ok("Message received and processed");
        }
    }
}