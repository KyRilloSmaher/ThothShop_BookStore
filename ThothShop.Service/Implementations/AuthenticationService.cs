using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Infrastructure.Implementions;
using ThothShop.Infrastructure.RepositoriesContracts;
using ThothShop.Service.Commans;
using ThothShop.Service.Contract;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace ThothShop.Service.implementations
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger<UserService> _logger;
        private readonly ApplicationDBContext _context;
        public AuthenticationService(
            JwtSettings jwtSettings,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            ApplicationDBContext context,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper,
            ILogger<UserService> logger)
        {
            _jwtSettings = jwtSettings;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _context = context;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
            _logger = logger;
        }

        public async Task<JwtResponse> GetJWTToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = await GetClaims(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                UserName = user.UserName,
                TokenString = Guid.NewGuid().ToString(),
                ExpireAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
            };

            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = false,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };

            await _refreshTokenRepository.AddAsync(userRefreshToken);

            return new JwtResponse
            {
                refreshToken = refreshToken,
                AccessToken = accessToken
            };
        }
        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userRepository.GetUserRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(accessToken);
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new SecurityTokenException("The access token does not contain a valid user ID.");

            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (storedRefreshToken == null)
                throw new SecurityTokenException("Refresh token not found or has been revoked.");

            if (storedRefreshToken.UserId != userId)
                throw new SecurityTokenException("The refresh token does not belong to the provided user.");

            if (storedRefreshToken.ExpiryDate <= DateTime.UtcNow)
                throw new SecurityTokenException("The refresh token has expired.");

            return (userId, storedRefreshToken.ExpiryDate);
        }


        public async Task<JwtResponse> RetrieveRefreshToken(User user, JwtSecurityToken jwtToken, string refreshToken, DateTime? expiryDate = null)
        {
            // Validate the old refresh token
            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (storedRefreshToken == null)
                throw new SecurityTokenException("Invalid refresh token");

            // Mark the old refresh token as used
            await _refreshTokenRepository.MarkTokenAsUsedAsync(refreshToken);

            // Clean up expired tokens
            await _refreshTokenRepository.DeleteExpiredTokensAsync();

            // Generate new tokens
            var newTokens = await GetJWTToken(user);

            // Revoke the old refresh token
            await _refreshTokenRepository.RevokeTokenAsync(refreshToken);

            return newTokens;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            try
            {
                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return "Valid";
            }
            catch (Exception ex)
            {
                return $"Invalid: {ex.Message}";
            }
        }





      





        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {


            var isValidToken = await _userRepository.ConfirmEmailAsync(email, code);
            if (!isValidToken)
                return false;

            return true;
        }
        public async Task<string> SendResetPasswordCode(string Email)
        {
         
            try
            {
                await _userRepository.BeginTransactionAsync();
                //user
                var user = await _userRepository.GetByEmailAsync(Email,true);
                //user not Exist => not found
                if (user == null)
                    return SystemMessages.NotFound;
                //Generate Random Number
                Random generator = new Random();
                string randomNumber = generator.Next(0, 1000000).ToString("D6");

                //update User In Database Code
                user.Code = randomNumber;
                var updateResult = await _userRepository.UpdateUserAsync(user);
                if (!updateResult.Succeeded)
                    return SystemMessages.Failed;
              var message = $@"
                              <html>
                              <head>
                                <style>
                                  body {{
                                    font-family: Arial, sans-serif;
                                    background-color: #f4f4f4;
                                    margin: 0;
                                    padding: 0;
                                  }}
                                  .container {{
                                    max-width: 600px;
                                    margin: 40px auto;
                                    background-color: #ffffff;
                                    padding: 30px;
                                    border-radius: 8px;
                                    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
                                  }}
                                  .header {{
                                    text-align: center;
                                    padding-bottom: 20px;
                                  }}
                                  .header h1 {{
                                    color: #333;
                                  }}
                                  .content {{
                                    font-size: 16px;
                                    line-height: 1.6;
                                    color: #555;
                                  }}
                                  .btn {{
                                    display: inline-block;
                                    margin-top: 25px;
                                    padding: 12px 25px;
                                    background-color: #007bff;
                                    color: #ffffff;
                                    text-decoration: none;
                                    border-radius: 5px;
                                    font-weight: bold;
                                  }}
                                  .footer {{
                                    margin-top: 30px;
                                    font-size: 12px;
                                    text-align: center;
                                    color: #888;
                                  }}
                                </style>
                              </head>
                              <body>
                                <div class='container'>
                                  <div class='header'>
                                    <h1>Reset YOur Password </h1>
                                  </div>
                                  <div class='content'>
                                    <p>Hi {user.UserName},</p>
                                    <p>You Can Use this Code Below To Reset Your Password</p>
                                    <p style='text-align:center;'>
                                       {randomNumber}
                                    </p>
                                    <p>If you didn’t create an account, you can safely ignore this email.</p>
                                  </div>
                                  <div class='footer'>
                                    &copy; {DateTime.UtcNow.Year} Thoth Store. All rights reserved.
                                  </div>
                                </div>
                              </body>
                              </html>";
                //Send Code To  Email 
                await _emailService.SendPasswordResetEmailAsync(user.Email, "Reset Password", message);
                await _userRepository.CommitTransactionAsync();
                return SystemMessages.Success;
            }
            catch (Exception ex)
            {
                await _userRepository.RollbackTransactionAsync();
                return "Failed";
            }
        }
        public async Task<string> ConfirmResetPassword(string email, string code)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return SystemMessages.NotFound;
            var userCode = user.Code;
            if (userCode == code) return SystemMessages.Success;
            return SystemMessages.Failed;
        }
        public async Task<string> ResetPassword(string Email, string Password)
        {
            try
            {
                await _userRepository.BeginTransactionAsync();
                var user = await _userRepository.GetByEmailAsync(Email, true);
                if (user == null)
                    return SystemMessages.NotFound;
                await _userRepository.RemovePasswordAsync(user);
                if (!await _userRepository.HasPasswordAsync(user))
                {
                    await _userRepository.AddPasswordAsync(user, Password);
                }
                await _userRepository.CommitTransactionAsync();
                return SystemMessages.Success;
            }
            catch (Exception ex)
            {
                await _userRepository.RollbackTransactionAsync();
                return SystemMessages.Failed;
            }
        }
        public async Task<string> ChangePasswordAsync(string Email, string CurrentPassword, string NewPassword)
        {
            var user = await _userRepository.GetByEmailAsync(Email, true);
            if (user == null)
                return SystemMessages.NotFound;
            var result = await _userRepository.ChangePasswordAsync(user, CurrentPassword, NewPassword);
            if (!result)
                return SystemMessages.Failed;
            return SystemMessages.Success;
        }
    }
}
