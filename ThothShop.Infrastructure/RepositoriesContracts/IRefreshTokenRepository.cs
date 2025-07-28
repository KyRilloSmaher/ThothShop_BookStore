using System;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.RepositoriesContracts
{
    public interface IRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
        Task<UserRefreshToken> GetByTokenAsync(string refreshToken);
        Task<UserRefreshToken> GetByJwtIdAsync(string jwtId);
        Task<bool> MarkTokenAsUsedAsync(string refreshToken);
        Task<bool> RevokeTokenAsync(string refreshToken);
        Task<bool> DeleteExpiredTokensAsync();
        Task<bool> IsTokenValid(string refreshToken);
    }
}