using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;

namespace ThothShop.Service.Contract
{
    public interface ICartService
    {
        Task<Cart?> GetCartByIdAsync(Guid Id ,bool tracked);
        Task<IEnumerable<Cart>> GetUserCartAsync(string userId);
        Task<IEnumerable<Cart>> GetAllCarts();
        Task<Guid> CreateCartAsync(Cart cart);
        Task<bool> AddToCartAsync(Guid Id, Guid bookId, int quantity = 1);
        Task<bool> RemoveFromCartAsync(Guid Id, Guid bookId);
        Task<bool> UpdateCartItemQuantityAsync(Guid Id, Guid bookId, int newQuantity);
        Task<bool> ClearCartAsync(Guid Id);
        Task<int> GetCartItemCountAsync(Guid Id);
        Task<decimal> GetCartTotalAsync(Guid Id);
        Task<IEnumerable<CartItem>> GetCartItemsAsync(Guid Id);
        Task<bool> DeleteCartAsync(Guid Id);
    }
}
