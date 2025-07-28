using AutoMapper;
using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Books.Queries.Responses;
using ThothShop.Core.Features.Orders.Queries.Models;
using ThothShop.Core.Features.Orders.Queries.Responses;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;
using ThothShop.Service.implementations;
using ThothShop.Service.Wrappers;

namespace ThothShop.Core.Features.Orders.Queries.Handlers
{
    public class OrderQueryHandler : ResponseHandler,
        IRequestHandler<GetOrderByIdQueryModel, Response<OrderResponse>>,
        IRequestHandler<GetUserOrdersQueryModel, Response<IEnumerable<OrderResponse>>>,
        IRequestHandler<GetOrdersByStatusQueryModel, Response<IEnumerable<OrderResponse>>>,
        IRequestHandler<GetAllOrderQueryModel, PaginatedResult<OrderResponse>>,
        IRequestHandler<GetTotalSalesQueryModel, Response<int>>,
        IRequestHandler<GetOrderCountQueryModel, Response<int>>,
        IRequestHandler<GetRecentOrdersQueryModel, Response<IEnumerable<OrderResponse>>>

    {
        #region Field(s)
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public OrderQueryHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        #endregion

        #region Method(s)
        public async Task<Response<OrderResponse>> Handle(GetOrderByIdQueryModel request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByIdAsync(request.Id, false);
            if (order == null)
                return NotFound<OrderResponse>("Order not found");

            var response = _mapper.Map<OrderResponse>(order);
            return Success(response);
        }

        public async Task<Response<IEnumerable<OrderResponse>>> Handle(GetUserOrdersQueryModel request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetUserOrdersAsync(request.UserId);
            var responses = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            return Success(responses);
        }

        public async Task<Response<IEnumerable<OrderResponse>>> Handle(GetOrdersByStatusQueryModel request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(request.Status);
            var responses = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            return Success(responses);
        }

        public async Task<Response<int>> Handle(GetTotalSalesQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetTotalSalesAsync();
            return Success<int>( (int)(Math.Ceiling(result)));
        }

        public async Task<Response<IEnumerable<OrderResponse>>> Handle(GetRecentOrdersQueryModel request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetRecentOrders();
            var responses = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            return Success(responses);
        }

        public async Task<Response<int>> Handle(GetOrderCountQueryModel request, CancellationToken cancellationToken)
        {
          var ordercount = await _orderService.GetOrderCountAsync();
          return Success<int>(ordercount);
        }

        public async Task<PaginatedResult<OrderResponse>> Handle(GetAllOrderQueryModel request, CancellationToken cancellationToken)
        {
            var query =  _orderService.GetAllOrdersAsync();
            var paginatedorders = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            var mappedOrderss = _mapper.Map<IEnumerable<OrderResponse>>(paginatedorders.Data);

            return PaginatedResult<OrderResponse>.Success(mappedOrderss, paginatedorders.TotalCount, paginatedorders.CurrentPage, paginatedorders.PageSize);
        }
        #endregion
    }
} 