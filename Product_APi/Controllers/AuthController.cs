using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Product_APi.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Product_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public static User user = new User();
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration= configuration;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (string.Equals(request.Username,"a")&& string.Equals(request.Password, "a"))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Username) };
                var jwt = new JwtSecurityToken(
               issuer: AuthOptions.ISSUER,
               audience: AuthOptions.AUDIENCE,
               claims: claims,
               expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
               signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            }
            return Unauthorized("Wrong credentials");
        }

   
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            return Ok();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
