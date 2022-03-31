using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Product_APi.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Product_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration,IUserRepository userRepository)
        {
            _configuration= configuration;
            _userRepository= userRepository;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var passwordHashDto  = GetHash(request.Password);
            var pass = _userRepository.GetUserPassword(passwordHashDto);
            if (pass == null)
                return NotFound();

            //var nameHashDto = GetHash(request.Username);
            var name = _userRepository.GetUserName(request.Username);
            if (name == null)
                return NotFound();

            if (string.Equals(request.Username, name) && string.Equals(passwordHashDto, pass))
            {
                var token = CreateToken(request);
               return Ok(CreateToken(request));
            }
            return Unauthorized("Wrong credentials");
        }

        public string CreateToken(UserDto request)
        {
               //var password = GetHash(request.Password);
               var name = GetHash(request.Username);

               var claims = new List<Claim> { new Claim(ClaimTypes.Name, name) };
               var jwt = new JwtSecurityToken(
               issuer: AuthOptions.ISSUER,
               audience: AuthOptions.AUDIENCE,
               claims: claims,
               expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
               signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
           
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(UserDto request)
        {
            var passwordHash = GetHash(request.Password);
            // var nameHash = GetHash(request.Username);
            var userDto = new UserDto();
            //userDto.Username = nameHash;
            userDto.Username = request.Username;
            userDto.Password = passwordHash;
            _userRepository.CreateUser(userDto);
            return Ok();
        }

        private string GetHash(string source)
        {
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(source));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        //private string CreateUserNamedHash(string password)
        //{
        //    string passwordHash;
        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)).ToString();
        //    }
        //    return passwordHash;
        //}

        //private bool VerifyPasswordHash(string password, byte[] passwordHash)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(passwordHash);
        //    }
        //}
    }
}
