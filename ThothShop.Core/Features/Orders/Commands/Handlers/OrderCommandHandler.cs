using AutoMapper;
using MediatR;
using Stripe.Checkout;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Orders.Commands.Models;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Orders.Commands.Handlers
{
    public class OrderCommandHandler : ResponseHandler,
        IRequestHandler<CreateOrderFromCartCommandModel, Response<Guid>>,
        IRequestHandler<UpdateOrderStatusCommandModel, Response<string>>,
        IRequestHandler<DeleteOrderCommandModel, Response<bool>>,
        IRequestHandler<CheckoutOrderCommandModel, Session>
    {
        #region Field(s)
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        private readonly IPaymentProcessService _paymentProcessService;
        private readonly IMapper _mapper;

        #endregion


        #region Constructor(s)
        public OrderCommandHandler(IOrderService orderService, IMapper mapper, IPaymentProcessService paymentProcessService, IBookService bookService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _paymentProcessService = paymentProcessService;
            _bookService = bookService;
        }

        #endregion

        #region Method(s)

        public async Task<Response<Guid>> Handle(CreateOrderFromCartCommandModel request, CancellationToken cancellationToken)
        {
            var order = await _orderService.CreateOrderFromCartIdAsync(request.CartId);
            if (order == null)
                return BadRequest<Guid>("Failed to create order from cart");

            return Success(order.Id);
        }

        public async Task<Response<string>> Handle(UpdateOrderStatusCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _orderService.UpdateOrderStatusAsync(request.Id, request.NewStatus);
            if (!result)
                return BadRequest<string>("Failed to update order status");

            return Success("Order status updated successfully");
        }

        public async Task<Response<bool>> Handle(DeleteOrderCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _orderService.DeleteOrderAsync(request.Id);
            if (!result)
                return NotFound<bool>("Order not found");

            return Success(true);
        }

        public  async Task<Session> Handle(CheckoutOrderCommandModel request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByIdAsync(request.OrderId);
            if (order == null)
                return null;

            var result = await _paymentProcessService.ProcessPayment(order, request.SuccessUrl, request.FailedUrl);
            foreach (var item in order.Items)
            {
               await  _bookService.DecreaseBookStock(item.Id , item.Quantity);
            }
            return result;
        }

        #endregion
    }
} 