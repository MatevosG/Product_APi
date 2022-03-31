using BLL.Contracts;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Product_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users =_userRepository.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user!=null)
            {
                return Ok(user);
            }
            return BadRequest("ther not user by that id");
        }
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var userForDelete = _userRepository.GetById(id);
            if (userForDelete != null)
            {
                _userRepository.DeleteUser(id);
                return Ok("successfuly deleted");
            }
            return BadRequest();
        }
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UserDto userDto)
        {
            var userForUpdate = _userRepository.GetById(userDto.Id);
            if (userForUpdate != null)
            {
                User user = new User();
                user.Name = userDto.Username;
                user.Password = userDto.Password;
                _userRepository.UpdateUser(user);
                return Ok("successfuly deleted");
            }
            return BadRequest();
        }
    }
}
