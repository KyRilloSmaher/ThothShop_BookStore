using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Service.Commans;

namespace ThothShop.Service.Contract
{
    public interface IAuthenticationService
    {
        Task<JwtResponse> GetJWTToken(User user);
        JwtSecurityToken ReadJWTToken(string accessToken);
        Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        Task<JwtResponse> RetrieveRefreshToken(User user, JwtSecurityToken jwtToken, string refreshToken, DateTime? expiryDate = null);
        Task<string> ValidateToken(string accessToken);
        Task<bool> ConfirmEmailAsync(string email, string code);
        Task<string> SendResetPasswordCode(string email);
        Task<string> ConfirmResetPassword(string email, string code);
        Task<string> ResetPassword(string email, string password);
        Task<string> ChangePasswordAsync(string Email, string CurrentPassword, string NewPassword);
    }
}
