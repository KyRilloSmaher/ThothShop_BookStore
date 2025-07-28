using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThothShop.Api.Bases;
using ThothShop.Core.Features.Users.Commands.Models;
using ThothShop.Core.Features.Users.Queries.Models;
using ThothShop.Domain.AppMetaData;
using ThothShop.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ThothShop.Api.Controllers
{
    [ApiController]
    public class UserController : ApplicationBaseController
    {
        public UserController() { }
    
        [HttpGet(Router.User.GetAllUsers)]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await Mediator.Send(new GetAllUsersQueryModel());
            return FinalResponse(response);
        }
        [HttpGet(Router.User.GetAllAdmins)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var response = await Mediator.Send(new GetAllAdminsQueryModel());
            return FinalResponse(response);
        }
        [Authorize(Roles = "Owner,Admin")]
        [HttpGet(Router.User.GetById)]
        public async Task<IActionResult> GetUserById([FromRoute] string Id)
        {
            var response = await Mediator.Send(new GetUserByIdQueryModel (Id ));
            return FinalResponse(response);
        }
        [Authorize(Roles = "User")]
        [HttpGet(Router.User.MyProfile)]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = await Mediator.Send(new GetUserByIdQueryModel(userId));
            return FinalResponse(response);
        }
        [HttpGet(Router.User.GetByUsername)]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string username)
        {
            var response = await Mediator.Send(new GetUserByUserNameQueryModel(username));
            return FinalResponse(response);
        }
        [HttpPost(Router.User.RegisterAdmin)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> RegisterAdmin([FromBody] CreateAdminCommandModel query)
        {
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }
        [HttpPost(Router.User.RegisterUser)]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommandModel query)
        {
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }
        [HttpPut(Router.User.UpdateUser)]
        [Authorize(Roles = "Owner,User")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandModel query)
        {
            var response = await Mediator.Send(query);
            return FinalResponse(response);
        }
        [HttpDelete(Router.User.DeleteUser)]
        [Authorize(Roles = "Owner,User")]
        public async Task<IActionResult> DeleteUser([FromRoute] string Id)
        {
            var response = await Mediator.Send(new DeleteUserCommandModel ( Id ));
            return FinalResponse(response);
        }
    }
}
