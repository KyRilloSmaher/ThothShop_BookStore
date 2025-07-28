using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.Authentications.Commands.Models;
using ThothShop.Core.Features.Authentications.Queries;
using ThothShop.Core.Features.Authentications.Queries.Models;
using ThothShop.Core.Features.Books.Queries.Models;
using ThothShop.Core.Features.Users.Commands.Models;
using ThothShop.Domain.AppMetaData;
using ThothShop.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : ApplicationBaseController
    {
        public AuthenticationController() { }
       
        [HttpGet(Router.Authentication.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string Email, string Code)
        {
            var query = new ConfirmEmailQueryModel
            {
                email = Email,
                code = Code
            };
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }
        [HttpPost(Router.Authentication.Login)]
        public async Task<IActionResult> Login([FromBody] LoginQueryModel query)
        {
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }
        [HttpPost(Router.Authentication.SendResetCode)]
        public async Task<IActionResult> SendResetCode([FromBody] SendResetCodeCommandModel query)
        {
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }
        [HttpPost(Router.Authentication.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommandModel query)
        {
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }
        [HttpPost(Router.Authentication.ConfirmResetPasswordCode)]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmResetPasswordQueryModel query)
        {
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }
        [HttpPost(Router.Authentication.ChangePassword)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommandModel query)
        {
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }
        [HttpPost(Router.Authentication.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandModel query)
        {
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }

    }
}
