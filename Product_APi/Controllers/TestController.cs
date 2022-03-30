using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Product_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet("Test1")]
        public IActionResult Test1()
        {
            return Ok("helooo");    
        }
        [HttpGet("Test2")]
        public IActionResult Test2()
        {
            return Ok("helooo");
        }
    }
}
