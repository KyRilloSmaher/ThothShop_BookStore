using AutoMapper;
using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Cart.Queries.Models;
using ThothShop.Core.Features.Carts.Queries.Models;
using ThothShop.Core.Features.Carts.Queries.Responses;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Cart.Queries.Handlers
{
    public class CartQueryHandler : ResponseHandler,
        IRequestHandler<GetUserCartsQueryModel, Response<IEnumerable<Cartresponse>>>,
        IRequestHandler<GetCartItemCountQueryModel, Response<int>>,
        IRequestHandler<GetCartTotalQueryModel, Response<decimal>>,
        IRequestHandler<GetAllCartsQuery, Response<IEnumerable<Cartresponse>>>,
        IRequestHandler<GetCartByIdQueryModel, Response<Cartresponse>>,
        IRequestHandler<GetCartItemsQueryModel, Response<IEnumerable<CartItemResponse>>>
    {
        #region Feild(s)
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        #endregion


        #region Constructor(s)
        public CartQueryHandler(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }
        #endregion


        #region Method(s)

        public async Task<Response<int>> Handle(GetCartItemCountQueryModel request, CancellationToken cancellationToken)
        {
            var count = await _cartService.GetCartItemCountAsync(request.CartId);
            return Success(count);
        }

        public async Task<Response<decimal>> Handle(GetCartTotalQueryModel request, CancellationToken cancellationToken)
        {
            var total = await _cartService.GetCartTotalAsync(request.CartId);
            return Success(total);
        }

        public async Task<Response<IEnumerable<Cartresponse>>> Handle(GetUserCartsQueryModel request, CancellationToken cancellationToken)
        {
            var carts = await _cartService.GetUserCartAsync(request.UserId);
            var cartResponses = _mapper.Map<IEnumerable<Cartresponse>>(carts);
            return Success(cartResponses);
        }
        public async Task<Response<IEnumerable<Cartresponse>>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            var carts = await _cartService.GetAllCarts();
            var cartResponses = _mapper.Map<IEnumerable<Cartresponse>>(carts);
            return Success(cartResponses);
        }

        public async Task<Response<Cartresponse>> Handle(GetCartByIdQueryModel request, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetCartByIdAsync(request.Id, false);
            if (cart == null)
                return NotFound<Cartresponse>("Cart not found");
            var cartResponse = _mapper.Map<Cartresponse>(cart);
            return Success(cartResponse);
        }

        public async Task<Response<IEnumerable<CartItemResponse>>> Handle(GetCartItemsQueryModel request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartService.GetCartItemsAsync(request.CartId);
            if (cartItems == null)
                return NotFound<IEnumerable<CartItemResponse>>("Cart items not found");
            var cartItemResponses = _mapper.Map<IEnumerable<CartItemResponse>>(cartItems);
            return Success(cartItemResponses);
        }

        #endregion
    }
} 