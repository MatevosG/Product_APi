using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetById(int id);
        User CreateUser(UserDto user);
        User UpdateUser(User user);
        void DeleteUser(int id);
        string GetUserPassword(string password);
        string GetUserName(string username);
    }
}
