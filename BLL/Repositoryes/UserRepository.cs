using BLL.Contracts;
using BLL.Models;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserRepository : IUserRepository
    {
        private ProductDbContext _context;
        public UserRepository(ProductDbContext context)
        {
            _context = context; 
        }

        public User CreateUser(UserDto userDto)
        {
            var user = MapUser(userDto);  
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        private User MapUser(UserDto userDto)
        {
            User user = new User();
            user.Name = userDto.Username;
            user.Password = userDto.Password;
            return user;
        }
        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public string GetUserName(string username)
        {
            var name = _context.Users.FirstOrDefault(x => x.Name == username)?.Name;
            return name;
        }

        public string GetUserPassword(string password)
        {
            var passwordDb = _context.Users.FirstOrDefault(x=>x.Password==password)?.Password;
            return password;
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
