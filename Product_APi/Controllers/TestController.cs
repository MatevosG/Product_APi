using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Product_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
       // [Authorize]
        [HttpGet("Test1")]
        public IActionResult Test1()
        {
            var usre = new UserDto();
            string data = JsonConvert.SerializeObject(usre);
            return Ok("helooo");    
        }
        [HttpGet("Test2")]
        public IActionResult Test2()
        {
            return Ok("helooo");
        }
    }
}
