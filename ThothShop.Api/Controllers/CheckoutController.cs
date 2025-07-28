using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.Orders.Commands.Models;
using ThothShop.Core.Features.Payments.Commands.Models;
using ThothShop.Domain.AppMetaData;
using ThothShop.Domain.Helpers;
using ThothShop.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Server.Controllers;

[ApiController]
[Authorize]

public class CheckoutController : ApplicationBaseController
{
    private readonly StripeSettings _stripeSettings;


    public CheckoutController(StripeSettings stripeSettings)
    {
        _stripeSettings = stripeSettings;
    }

    [HttpPost(Router.CheckOut.checkout)]
    public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutOrderCommandModel command, [FromServices] IServiceProvider sp)
    {
        if (sp == null) {
            return BadRequest("Service provider is null");
        }
        var server = sp.GetService<IServer>();
        if (server == null)
        {
            return BadRequest("Server is null");
        }
        var addresses = server.Features.Get<IServerAddressesFeature>();
        if (addresses == null)
        {
            return BadRequest("Server addresses are null");
        }
        var address = addresses.Addresses.FirstOrDefault();
        if (address == null)
        {
            return BadRequest("Server address is null");
        }
        command.SuccessUrl = address +"/"+ Router.CheckOut.Prefix+"/success?session_id={CHECKOUT_SESSION_ID}";
        command.FailedUrl = address +"/" +Router.CheckOut.Prefix + "/failed";
        command.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var result = await Mediator.Send(command);
        return Redirect(result.Url);
    }


    [HttpGet(Router.CheckOut.Success)]
    public async Task<IActionResult> CheckoutSuccess([FromQuery]string session_id , [FromQuery]Guid orderId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(session_id);
        if (session == null) { }
        // Handle the successful checkout here.
        var command = new SavePaymentCommandModel() {

            TransactionId = session.Id,
            OrderId = orderId,
            Amount = (decimal)session.AmountTotal,
        };
        var result = await Mediator.Send(command);
        return FinalResponse(result);
    }
    [HttpGet(Router.CheckOut.Failed)]
    public async Task<IActionResult> CheckoutFailed()
    {
        return BadRequest();
    }
}