using BLL.Cache;
using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Product_APi.Authorization;
using Product_APi.Blacist;
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
        private readonly IUserRepository _userRepository;
        private readonly ICacheRepository _cacheRepository;

        public AuthController(IUserRepository userRepository,ICacheRepository cacheRepository)
        {
            _userRepository = userRepository;
            _cacheRepository = cacheRepository;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var passwordHashDto = GetHash(request.Password);
            var pass = _userRepository.GetUserPassword(passwordHashDto);
            if (pass == null)
                return NotFound();

            var name = _userRepository.GetUserName(request.Username);
            if (name == null)
                return NotFound();

            if (string.Equals(request.Username, name) && string.Equals(passwordHashDto, pass))
            {
                var token = CreateToken(request);
                BlackList blacest = new BlackList();
                blacest.IsValid = true;
                blacest.UserId= request.Id.ToString();
                blacest.Token = token;
                var length = token.Length;  
                _cacheRepository.SetOrUpdate(blacest.Token, blacest);
               var sendtoken = new  { Token = token };
                return Ok(sendtoken);
            }
            return Unauthorized("Wrong credentials");
        }

        [HttpPost("LogOut")]
        public async Task<ActionResult<UserDto>> LogOut()
        {
            var tokenPeir = Request.Headers?.FirstOrDefault(x => x.Key == "Authorization");
            var token = tokenPeir.Value.Value.FirstOrDefault()?.Replace("Bearer ","");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("wrong token");
            }
            var blacetsFromCache = _cacheRepository.Get<BlackList>(token);
            blacetsFromCache.IsValid = false;
            _cacheRepository.SetOrUpdate(blacetsFromCache.Token, blacetsFromCache);
            return Ok("you are logged out");
        }

        public string CreateToken(UserDto request)
        {
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
            var pass = _userRepository.GetUserPassword(passwordHash);
            if (string.Equals(passwordHash,pass))
                return BadRequest();
                
            if(request.Username==_userRepository.GetUserName(request.Username))
                return BadRequest();

            request.Password = passwordHash;
            _userRepository.CreateUser(request);
            return Ok();
        }

        private string GetHash(string source)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(source));

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

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

