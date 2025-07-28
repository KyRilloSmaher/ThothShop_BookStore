using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;

namespace ThothShop.Service.Contract
{
    public interface IUserService
    {
        Task<User?> Login(string email, string password);
        Task<string> AddUser(User user ,string password ,string Role);
        Task<User> GetUserByIdAsync(string id, bool trackChanges = false);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string id);
        Task<IEnumerable<User>> GetAllAdmins();
    }
}
