using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.Contracts
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<IEnumerable<CartItem>> GetCartItemsByCartIdIdAsync(Guid Id);
        Task DeleteByCartIdAsync(Guid Id);
        Task<IEnumerable<Cart>> GetCartsByUserIdAsync(string userId);
        Task<bool> IsCartExistAsync(Guid Id);
        Task<bool> IsCartItemExistAsync(Guid cartId, Guid bookId);
        Task<bool> AddItemToCartAsync(CartItem cartItem);
        Task<bool> RemoveCartItemAsync(CartItem cartItem);

    }
}
