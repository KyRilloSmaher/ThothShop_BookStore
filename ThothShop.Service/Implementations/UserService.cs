
using ThothShop.Service.Contract;
using Microsoft.Extensions.Logging;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using Microsoft.AspNetCore.Identity;
using ThothShop.Infrastructure.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ThothShop.Domain.Helpers;
using ThothShop.Service.Commans;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace ThothShop.Service.implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailService _emailService;
        private readonly ApplicationDBContext _context;

        public UserService(
            IUserRepository userRepository,
            IAuthenticationService authenticationService,
            ILogger<UserService> logger,
            ApplicationDBContext context,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _logger = logger;
            _context = context;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
        }
        public async Task<User> GetUserByIdAsync(string id, bool trackChanges = false)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("User ID cannot be empty");

            var user = await _userRepository.GetByIdAsync(id, trackChanges);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: {id}");
                throw new KeyNotFoundException($"User not found with ID: {id}");
            }

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning($"User not found with email: {email}");
                throw new KeyNotFoundException($"User not found with email: {email}");
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
        public async Task<IEnumerable<User>> GetAllAdmins()
        {
            var admins = await _userRepository.GetAllAdminsAsync();
            return admins;
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var result =await _userRepository.UpdateUserAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogError($"Error Happend when update  user: {user.Id}");
                throw new Exception("Error Happend when update  user");
            }
            return true;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("User ID cannot be empty");

            var user = await _userRepository.GetByIdAsync(id,true);
            if (user == null)
            {
                _logger.LogWarning($"Attempted to delete non-existent user: {id}");
                throw new KeyNotFoundException($"User not found with ID: {id}");
            }

            await _userRepository.DeleteUserAsync(user);
            return true;
        }
        public async Task<string> AddUser(User user, string password, string Role)
        {
            try
            {
                if (user is null)
                {
                    throw new ArgumentNullException(nameof(user));

                }
                await _userRepository.BeginTransactionAsync();
                //Check If Email is Exists
                var existUser = await _userRepository.GetByEmailAsync(user.Email);
                if (existUser != null) return SystemMessages.EmailAlreadyExists;

                //Check If Username Exists
                var userByUserName = await _userRepository.GetByUsernameAsync(user.UserName);
                if (userByUserName != null) return SystemMessages.UserNameAlreadyExists;

                //Create User
                var result = await _userRepository.CreateUserAsync(user, password);
                //Failed to Create User
                if (!result.Succeeded)
                    return string.Join(",", result.Errors.Select(x => x.Description).ToList());

                await _userRepository.AddToRoleAsync(user,Role);

                //Send Confirm Email
                var code = await _userRepository.GenerateEmailConfirmationTokenAsync(user);
                var resquestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { email = user.Email, code = code });
                await _emailService.SendConfirmationEmailAsync(user.Email, returnUrl);

                await _userRepository.CommitTransactionAsync();
                return SystemMessages.Success;

            }
            catch (Exception ex)
            {
                _userRepository.RollbackTransactionAsync();
                _logger.LogError(ex, "Error occurred while adding user");
                return SystemMessages.FailedToAddEntity;
            }

        }

        public async Task<User?> Login(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
                return null;
            var isValidPassword = await _userRepository.CheckPasswordAsync(user, password);
            if (!isValidPassword)
                return null;
            if (!user.EmailConfirmed)
                return null;

            return user;
        }

    }
}