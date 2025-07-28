using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;

namespace ThothShop.Service.Contract
{
    public interface IWishListService
    {
        Task<Wishlist> GetByIdAsync(Guid id);
        Task<IEnumerable<Wishlist>> GetUserWishListAsync(string userId);
        Task<bool> AddToWishListAsync(Guid bookId, string userId);
        Task<bool> RemoveFromWishListAsync(Guid bookId, string userId);
        Task<bool> IsInWishListAsync(Guid bookId, string userId);
        public Task<int> GetWishListItemCountAsync(string userId);
        public Task ClearWishListAsync(string userId);
    }
}
