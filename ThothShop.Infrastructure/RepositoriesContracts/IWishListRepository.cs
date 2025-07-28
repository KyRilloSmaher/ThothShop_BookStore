using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.Contracts
{
    public interface IWishListRepository :IGenericRepository<Wishlist>
    {
        Task<IEnumerable<Wishlist>> GetUserWishlistAsync(string userId);
        Task<bool> ItemExistsInWishlistAsync(string userId, Guid bookId);
        Task ClearUserWishlistAsync(string userId);
        Task AddToWishlistAsync(string userId, Guid bookId);
        Task RemoveFromWishlistAsync(string userId, Guid bookId);
        Task<int> GetWishlistItemCountAsync(string userId);
    }
}
