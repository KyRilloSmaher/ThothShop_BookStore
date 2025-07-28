using AutoMapper;
using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Carts.Commands.Models;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Cart.Commands.Handlers
{
    public class CartCommandHandler : ResponseHandler,
        IRequestHandler<AddToCartCommandModel, Response<string>>,
        IRequestHandler<ClearCartCommandModel, Response<string>>,
        IRequestHandler<CreateCartCommandModel, Response<Guid>>,
        IRequestHandler<DeleteCartCommandModel, Response<bool>>,
        IRequestHandler<RemoveFromCartCommandModel, Response<string>>,
        IRequestHandler<UpdateCartItemQuantityCommandModel, Response<string>>
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartCommandHandler(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddToCartCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _cartService.AddToCartAsync(request.CartId, request.BookId, request.Quantity);
            if (!result)
                return BadRequest<string>("Failed to add item to cart");

            return Success("Item added to cart successfully");
        }

        public async Task<Response<string>> Handle(RemoveFromCartCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _cartService.RemoveFromCartAsync(request.CartId, request.BookId);
            if (!result)
                return NotFound<string>("Item not found in cart");

            return Success("Item removed from cart successfully");
        }

        public async Task<Response<string>> Handle(UpdateCartItemQuantityCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _cartService.UpdateCartItemQuantityAsync(request.CartId, request.BookId, request.NewQuantity);
            if (!result)
                return BadRequest<string>("Failed to update cart item quantity");

            return Success("Cart item quantity updated successfully");
        }

        public async Task<Response<string>> Handle(ClearCartCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _cartService.ClearCartAsync(request.Id);
            if (!result)
                return BadRequest<string>("Failed to clear cart");

            return Success("Cart cleared successfully");
        }

        public async  Task<Response<Guid>> Handle(CreateCartCommandModel request, CancellationToken cancellationToken)
        {
            var cart = _mapper.Map<Domain.Models.Cart>(request);
            var result = await _cartService.CreateCartAsync(cart);
            if (result == Guid.Empty)
                return BadRequest<Guid>("Failed to create cart");

            return Success(result);
        }

        public async Task<Response<bool>> Handle(DeleteCartCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _cartService.DeleteCartAsync(request.CartId);
            if (!result)
                return NotFound<bool>("Cart not found");
            return Success(true);
        }
    }
} 