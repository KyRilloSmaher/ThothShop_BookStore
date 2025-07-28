using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Service.Contract;
namespace ThothShop.Service.implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IBookRepository _bookRepository;

        public CartService(ICartRepository cartRepository, IBookRepository bookRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            var carts = await _cartRepository.GetAll();
            return carts;
        }
        public async Task<Cart?> GetCartByIdAsync(Guid Id, bool tracked)
        {
            var cart = await _cartRepository.GetByIdAsync(Id, tracked);
            return cart;
        }


        public async Task<bool> AddToCartAsync(Guid CartId, Guid bookId, int quantity = 1)
        {
            if (quantity <= 0)
                return false;

            var cart = await _cartRepository.GetByIdAsync(CartId);
            if (cart == null) return false;



            var existingItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;

            }
            else{

                cart.Items.Add(new CartItem
                {
                    BookId = bookId,
                    Quantity = quantity,
                    UnitPrice = _bookRepository.GetByIdAsync(bookId).Result.Price
                });
                cart.TotalPrice = cart.CalculateTotal();
                await _cartRepository.UpdateAsync(cart);
               
            }

            return true;


        }

        public async Task<bool> ClearCartAsync(Guid Id)
        {
            var cart = await _cartRepository.GetByIdAsync(Id,true);
            if (cart == null)
                return false;

            cart.Items.Clear();
            cart.TotalPrice = 0;
            await _cartRepository.UpdateAsync(cart);
            return true;
        }

        public async Task<int> GetCartItemCountAsync(Guid Id)
        {
            var cart = await _cartRepository.GetByIdAsync(Id,false);
            return cart?.Items.Sum(i => i.Quantity) ?? 0;
        }

        public async Task<decimal> GetCartTotalAsync(Guid Id)
        {
            var cart = await _cartRepository.GetByIdAsync(Id, false);
            return cart?.TotalPrice ?? 0;
        }

        public async Task<IEnumerable<Cart>> GetUserCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartsByUserIdAsync(userId);
            return cart;
        }

        public async Task<bool> RemoveFromCartAsync(Guid Id, Guid bookId)
        {
            var ExistingCart = await _cartRepository.GetByIdAsync(Id, true);
            if (ExistingCart == null)
                return false;
            var item = ExistingCart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item == null)
                return false;
            ExistingCart.Items.Remove(item);
            ExistingCart.TotalPrice = ExistingCart.CalculateTotal();
            await _cartRepository.UpdateAsync(ExistingCart);
            return true;
        }

        public async Task<bool> UpdateCartItemQuantityAsync(Guid Id, Guid bookId, int newQuantity)
        {
            var cart = await _cartRepository.GetByIdAsync(Id, true);
            if (cart == null)
                return false;
            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item == null)
                return false;
            if (newQuantity <= 0)
            {
                cart.Items.Remove(item);
            }
            else
            {
                item.Quantity = newQuantity;
            }

            cart.TotalPrice = cart.CalculateTotal();
            await _cartRepository.UpdateAsync(cart);
            return true;

        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(Guid Id)
        {
          return await _cartRepository.GetCartItemsByCartIdIdAsync(Id);
        }

        public async Task<Guid> CreateCartAsync(Cart cart)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));
            await _cartRepository.AddAsync(cart);
            return cart.Id;
        }
        public async Task<bool> DeleteCartAsync(Guid Id)
        {
            var cart = await _cartRepository.GetByIdAsync(Id, true);
            if (cart == null)
                return false;
            await _cartRepository.DeleteByCartIdAsync(Id);
            return true;
        }
    }
}
