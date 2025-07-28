using Microsoft.EntityFrameworkCore;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Service.Contract;

namespace ThothShop.Service.Implementations
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository wishListRepository;

        public WishListService(IWishListRepository wishListRepository)
        {
            this.wishListRepository = wishListRepository;
        }

        public async Task<Wishlist> GetByIdAsync(Guid id)
        {
            return await wishListRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Wishlist>> GetUserWishListAsync(string userId)
        {
            return await wishListRepository.GetUserWishlistAsync(userId);
        }

        public async Task<bool> AddToWishListAsync(Guid bookId, string userId)
        {
            if (await IsInWishListAsync(bookId, userId))
                return false;

            var wishListItem = new Wishlist
            {
                BookId = bookId,
                UserId = userId,
            };

            await  wishListRepository.AddAsync(wishListItem);
            return  true;
        }

        public async Task<bool> RemoveFromWishListAsync(Guid bookId, string userId)
        {
            var wishListItem = await IsInWishListAsync(bookId, userId);

            if (!wishListItem)
                return false;
            var itemToRemove = await wishListRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(w => w.BookId == bookId && w.UserId == userId);
            if (itemToRemove == null)
                return false;
            await wishListRepository.DeleteAsync(itemToRemove);

            return true;
        }

        public async Task<bool> IsInWishListAsync(Guid bookId, string userId)
        {
            return await wishListRepository.ItemExistsInWishlistAsync(userId, bookId);
        }

        public async Task<int> GetWishListItemCountAsync(string userId)
        {
            return await  wishListRepository.GetWishlistItemCountAsync(userId);
        }

        public async Task ClearWishListAsync(string userId)
        {
            await wishListRepository.ClearUserWishlistAsync(userId);

        }
    }
}
