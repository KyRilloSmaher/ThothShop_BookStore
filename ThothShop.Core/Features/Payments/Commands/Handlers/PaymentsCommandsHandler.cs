using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Payments.Commands.Models;
using ThothShop.Domain.Models;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Payments.Commands.Handlers
{
    public class PaymentsCommandsHandler : ResponseHandler ,
        IRequestHandler<SavePaymentCommandModel, Response<string>>
    {
        #region Field(s)
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor(s)
        public PaymentsCommandsHandler(IPaymentService paymentService, IMapper mapper, IOrderService orderService)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _orderService = orderService;
        }
        #endregion
        #region Method(s)
        public async Task<Response<string>> Handle(SavePaymentCommandModel request, CancellationToken cancellationToken)
        {
            var mappedModel = _mapper.Map<Payment>(request);
            var result = await _paymentService.CreatePaymentAsync(mappedModel);
            if (result == null)
                return BadRequest<string>("Failed to save payment");
            var UpdateOrderStatus = _orderService.UpdateOrderStatusToCompletedAsync(request.OrderId,Domain.Enums.OrderStatus.Completed,result.Id);
            return Success("Payment saved successfully");
        }
        #endregion

    }
}
