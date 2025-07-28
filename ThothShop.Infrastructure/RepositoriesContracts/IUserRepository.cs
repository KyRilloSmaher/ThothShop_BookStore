using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Contracts
{
    public interface IUserRepository
    {
        // Transaction Management
        Task BeginTransactionAsync();

        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
      
        // Basic User Operations
        Task<User?> GetByIdAsync(string userId, bool trackChanges = false);
        Task<User?> GetByEmailAsync(string email, bool trackChanges = false);
        Task<User?> GetByUsernameAsync(string username, bool trackChanges = false);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetAllAdminsAsync();
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<IdentityResult> DeleteUserAsync(User user);

        // User Verification
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsUsernameUniqueAsync(string username);

        // Password Management
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<bool> ResetPasswordAsync(User user, string token, string newPassword);
        Task RemovePasswordAsync(User user);
        Task<bool> HasPasswordAsync(User user);
        Task<IdentityResult> AddPasswordAsync(User user, string password);
        // Role Management
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<bool> AddToRoleAsync(User user, string role);
        Task<bool> RemoveFromRoleAsync(User user, string role);
        Task<bool> IsInRoleAsync(User user, string role);

        // Email Confirmation
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<bool> ConfirmEmailAsync(string email, string token);
        Task<bool> IsEmailConfirmedAsync(User user);

        // Lockout/Account Status
        Task<bool> SetLockoutEnabledAsync(User user, bool enabled);
        Task<bool> IsLockedOutAsync(User user);
        Task<DateTimeOffset?> GetLockoutEndDateAsync(User user);
        Task<bool> SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd);
        Task<int> GetAccessFailedCountAsync(User user);
        Task<bool> ResetAccessFailedCountAsync(User user);
    }
}