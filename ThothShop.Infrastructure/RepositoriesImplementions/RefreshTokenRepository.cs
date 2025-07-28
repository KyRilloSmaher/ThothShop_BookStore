using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.RepositoriesContracts;

namespace ThothShop.Infrastructure.Implementations
{
    public class RefreshTokenRepository : GenericRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        private readonly ApplicationDBContext _context;

        public RefreshTokenRepository(ApplicationDBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserRefreshToken> GetByTokenAsync(string refreshToken)
        {
            return await _context.UserRefreshTokens
                .FirstOrDefaultAsync(rt => rt.RefreshToken == refreshToken);
        }

        public async Task<UserRefreshToken> GetByJwtIdAsync(string jwtId)
        {
            return await _context.UserRefreshTokens
                .FirstOrDefaultAsync(rt => rt.JwtId == jwtId);
        }

        public async Task<bool> MarkTokenAsUsedAsync(string refreshToken)
        {
            var token = await GetByTokenAsync(refreshToken);
            if (token == null) return false;

            token.IsUsed = true;
            _context.UserRefreshTokens.Update(token);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var token = await GetByTokenAsync(refreshToken);
            if (token == null) return false;

            token.IsRevoked = true;
            _context.UserRefreshTokens.Update(token);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteExpiredTokensAsync()
        {
            var expiredTokens = _context.UserRefreshTokens
                .Where(rt => rt.ExpiryDate < DateTime.UtcNow);

            _context.UserRefreshTokens.RemoveRange(expiredTokens);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsTokenValid(string refreshToken)
        {
            var token = await GetByTokenAsync(refreshToken);
            if (token == null) return false;

            return !token.IsUsed &&
                   !token.IsRevoked &&
                   token.ExpiryDate >= DateTime.UtcNow;
        }
    }
}