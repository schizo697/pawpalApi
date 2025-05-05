using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private static string _alertMessage = null;

        [HttpPost("alert")]
        public IActionResult PostAlert([FromBody] string status)
        {
            _alertMessage = status;
            return Ok($"Alert received: {status}");
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(_alertMessage ?? "NO_ALERT");
        }
    }
}
