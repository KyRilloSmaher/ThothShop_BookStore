using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.Contracts;

namespace ThothShop.Infrastructure.Implementions
{
        public class UserRepository : IUserRepository
        {
            private readonly UserManager<User> _userManager;
            private readonly ApplicationDBContext _context;

            public UserRepository(
                UserManager<User> userManager,
                ApplicationDBContext context)
            {
                _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task<User?> GetByIdAsync(string userId, bool trackChanges = false)
        {
            return trackChanges
                ? await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId)
                : await _userManager.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetByEmailAsync(string email, bool trackChanges = false)
        {
            return trackChanges
                ? await _userManager.Users.SingleOrDefaultAsync(u => u.Email == email)
                : await _userManager.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetByUsernameAsync(string username, bool trackChanges = false)
        {
            return trackChanges
                ? await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username)
                : await _userManager.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.GetUsersInRoleAsync("User");
        }
        public async Task<IEnumerable<User>> GetAllAdminsAsync()
        {
            return await _userManager.GetUsersInRoleAsync("Admin");
        }

        // Basic User Operations

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
            {
                var result = await _userManager.CreateAsync(user, password);
                return result;
            }

            public async Task<IdentityResult> UpdateUserAsync(User user)
            {
                var result = await _userManager.UpdateAsync(user);
                return result;
            }

            public async Task<IdentityResult> DeleteUserAsync(User user)
            {
                var result = await _userManager.DeleteAsync(user);
                return result;
            }

            public async Task<bool> IsEmailUniqueAsync(string email)
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user == null;
            }

            public async Task<bool> IsUsernameUniqueAsync(string username)
            {
                var user = await _userManager.FindByNameAsync(username);
                return user == null;
            }

            public async Task<bool> CheckPasswordAsync(User user, string password)
            {
                return await _userManager.CheckPasswordAsync(user, password);
            }

            public async Task<bool> ChangePasswordAsync(User user, string currentPassword, string newPassword)
            {
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                return result.Succeeded;
            }

            public async Task<string> GeneratePasswordResetTokenAsync(User user)
            {
                return await _userManager.GeneratePasswordResetTokenAsync(user);
            }

            public async Task<bool> ResetPasswordAsync(User user, string token, string newPassword)
            {
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                return result.Succeeded;
            }

            public async Task<IList<string>> GetUserRolesAsync(User user)
            {
                return await _userManager.GetRolesAsync(user);
            }

            public async Task<bool> AddToRoleAsync(User user, string role)
            {
                var result = await _userManager.AddToRoleAsync(user, role);
                return result.Succeeded;
            }

            public async Task<bool> RemoveFromRoleAsync(User user, string role)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role);
                return result.Succeeded;
            }

            public async Task<bool> IsInRoleAsync(User user, string role)
            {
                return await _userManager.IsInRoleAsync(user, role);
            }

            public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
            {
                return await _userManager.GenerateEmailConfirmationTokenAsync(user);
            }

            public async Task<bool> ConfirmEmailAsync(string email, string token)
            {
                 var user = await GetByEmailAsync(email,true);

            if (user != null)
            {
               // _context.Entry(user).State = EntityState.Detached;

                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
                return result.Succeeded;
            }
            return false;
        }

            public async Task<bool> IsEmailConfirmedAsync(User user)
            {
                return await _userManager.IsEmailConfirmedAsync(user);
            }

            public async Task<bool> SetLockoutEnabledAsync(User user, bool enabled)
            {
                var result = await _userManager.SetLockoutEnabledAsync(user, enabled);
                return result.Succeeded;
            }

            public async Task<bool> IsLockedOutAsync(User user)
            {
                return await _userManager.IsLockedOutAsync(user);
            }

            public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user)
            {
                return await _userManager.GetLockoutEndDateAsync(user);
            }

            public async Task<bool> SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd)
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
                return result.Succeeded;
            }

            public async Task<int> GetAccessFailedCountAsync(User user)
            {
                return await _userManager.GetAccessFailedCountAsync(user);
            }

            public async Task<bool> ResetAccessFailedCountAsync(User user)
            {
                var result = await _userManager.ResetAccessFailedCountAsync(user);
                return result.Succeeded;
            }

            // Additional custom methods not directly available in UserManager
            public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
            {
                return await _userManager.GetUsersInRoleAsync(role);
            }

            public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
            {
                return await _userManager.Users
                    .Where(u =>
                        u.Email.Contains(searchTerm) ||
                        u.UserName.Contains(searchTerm))
                    .ToListAsync();
            }

        public async Task RemovePasswordAsync(User user)
        {
            await _userManager.RemovePasswordAsync(user);
        }

        public async Task<bool> HasPasswordAsync(User user)
        {
            return await _userManager.HasPasswordAsync(user);
        }

        public Task<IdentityResult> AddPasswordAsync(User user, string password)
        {
            var result = _userManager.AddPasswordAsync(user, password);
            return result;
        }
    }
    
}
