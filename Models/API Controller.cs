using Microsoft.AspNetCore.Mvc;

namespace BabyNI.Models
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyController : ControllerBase
    {
        private readonly Context context;

        public DailyController(Context _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello, API!");
        }

        // Add your actions here
    }
}
