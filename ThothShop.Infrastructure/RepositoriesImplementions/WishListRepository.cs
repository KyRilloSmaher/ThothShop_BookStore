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
    public class WishListRepository: GenericRepository<Wishlist> , IWishListRepository
    {
        private readonly ApplicationDBContext _context;
        public WishListRepository(ApplicationDBContext context) : base(context)
        {
            _context = context; _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddToWishlistAsync(string userId, Guid bookId)
        {
            if (await ItemExistsInWishlistAsync(userId, bookId))
                return;

            var wishlistItem = new Wishlist
            {
                UserId = userId,
                BookId = bookId,
            };

            await _context.Wishlists.AddAsync(wishlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task ClearUserWishlistAsync(string userId)
        {
            var items = await _context.Wishlists
             .Where(w => w.UserId == userId)
             .ToListAsync();

            _context.Wishlists.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Wishlist>> GetUserWishlistAsync(string userId)
        {
            return await _context.Wishlists
            .Where(w => w.UserId == userId)
            .Include(w => w.Book)
            .ThenInclude(b => b.BookImages)
            .ThenInclude(i=>i.Image)
            .OrderByDescending(w => w.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<int> GetWishlistItemCountAsync(string userId)
        {
          return await _context.Wishlists.CountAsync(w=>w.UserId == userId);
        }

        public async Task<bool> ItemExistsInWishlistAsync(string userId, Guid bookId)
        {
            return await _context.Wishlists
           .AnyAsync(w => w.UserId == userId && w.BookId == bookId);
        }

        public async Task RemoveFromWishlistAsync(string userId, Guid bookId)
        {
            var item = await _context.Wishlists
             .FirstOrDefaultAsync(w => w.UserId == userId && w.BookId == bookId);

            if (item != null)
            {
                _context.Wishlists.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public new async Task<Wishlist> GetByIdAsync(Guid id)
        {
            return await _context.Wishlists.Include(w => w.Book).ThenInclude(b=>b.BookImages).Include(w => w.User).FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}
