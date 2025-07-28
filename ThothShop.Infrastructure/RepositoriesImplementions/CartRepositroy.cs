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
    public class CartRepositroy : GenericRepository<Cart>, ICartRepository
    {
        private readonly ApplicationDBContext _Context;
        public CartRepositroy(ApplicationDBContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<bool> AddItemToCartAsync(CartItem cartItem)
        {
            if (cartItem == null) {
                throw new ArgumentNullException(nameof(cartItem));
            }
            var cart = await _Context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartItem.CartId);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found");
            }
            var existingItem = cart.Items.FirstOrDefault(i => i.BookId == cartItem.BookId);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
            }
            else
            {
                await _Context.CartItems.AddAsync(cartItem);
                cart.Items.Add(cartItem);
            }
            cart.TotalPrice = cart.CalculateTotal();
            _Context.Carts.Update(cart);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async  Task DeleteByCartIdAsync(Guid Id)
        {
            var cart = await _Context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == Id);
            if (cart != null)
            {
                _Context.Carts.Remove(cart);
                await _Context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Cart not found");
            }

        }

        public override async Task<Cart?> GetByIdAsync(Guid id, bool tracked = false)
        {
            return tracked
                ? await _Context.Carts.Include(c => c.Items).ThenInclude(i=>i.Book).ThenInclude(b=>b.BookImages).ThenInclude(bi => bi.Image).FirstOrDefaultAsync(c => c.Id == id)
                : await _Context.Carts.AsNoTracking().Include(c => c.Items).ThenInclude(i => i.Book).ThenInclude(b => b.BookImages).ThenInclude(bi => bi.Image).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdIdAsync(Guid Id)
        {
            var cart = await _Context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == Id);
            if (cart != null)
            {
                return cart.Items;
            }
            else
            {
                throw new InvalidOperationException("Cart not found");
            }
        }

        public async  Task<IEnumerable<Cart>> GetCartsByUserIdAsync(string userId)
        {
            var carts = await _Context.Carts
                .Include(c => c.Items)
                .Where(c => c.UserId == userId).AsNoTracking()
                .ToListAsync();
            return carts;
        }

        public async Task<bool> IsCartExistAsync(Guid Id)
        {
            var cart = await _Context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == Id);
            return cart != null;
        }

        public async Task<bool> IsCartItemExistAsync(Guid cartId, Guid bookId)
        {
            var cartItem = await _Context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.BookId == bookId);
            return cartItem != null;
        }

        public async Task<bool> RemoveCartItemAsync(CartItem cartItem)
        {
            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }
            var cart = await _Context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartItem.CartId);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found");
            }
            var existingItem = cart.Items.FirstOrDefault(i => i.BookId == cartItem.BookId);
            if (existingItem != null)
            {
                cart.Items.Remove(existingItem);
                cart.TotalPrice = cart.CalculateTotal();
                _Context.Carts.Update(cart);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new InvalidOperationException("Cart item not found");
            }
        }
    }
}
