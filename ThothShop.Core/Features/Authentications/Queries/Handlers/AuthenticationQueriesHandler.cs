using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Authentications.Queries.Models;
using ThothShop.Domain.Helpers;
using ThothShop.Service.Commans;
using ThothShop.Service.Contract;

namespace ThothShop.Core.Features.Authentications.Queries.Handlers
{
    public class AuthenticationQueriesHandler : ResponseHandler
                                      , IRequestHandler<ConfirmEmailQueryModel, Response<string>>
                                      , IRequestHandler<LoginQueryModel, Response<JwtResponse>>
                                      , IRequestHandler<ConfirmResetPasswordQueryModel, Response<string>>
    {
        #region Field(s)
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        #endregion

        #region Constructor(s)
        public AuthenticationQueriesHandler(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        #endregion

        #region HandleMethod(s)

        public async Task<Response<string>> Handle(ConfirmEmailQueryModel request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _authenticationService.ConfirmEmailAsync(request.email, request.code);
            if (!confirmEmail)
                return BadRequest<string>("ErrorWhenConfirmEmail");
            return Success("ConfirmEmailDone");
        }

        public async Task<Response<JwtResponse>> Handle(LoginQueryModel request, CancellationToken cancellationToken)
        {
            var user = await _userService.Login(request.Email ,request.Password);
            if (user == null)
                return NotFound<JwtResponse>(SystemMessages.LoginFailed);
            var token = await _authenticationService.GetJWTToken(user);
            if (token == null)
                return NotFound<JwtResponse>(SystemMessages.LoginFailed);
            return Success<JwtResponse>(token);
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQueryModel request, CancellationToken cancellationToken)
        {
           var confirmResetPassword = await _authenticationService.ConfirmResetPassword(request.Email, request.Code);
            switch (confirmResetPassword)
            {
                case SystemMessages.NotFound:
                    return BadRequest<string>(string.Format(SystemMessages.EntityNotFound, "User"));
                case SystemMessages.Failed:
                    return BadRequest<string>(string.Format(SystemMessages.Failed, "Confrimation Failed"));
                case SystemMessages.Success:
                    break;

            }
            return Success("ConfirmResetPasswordDone");
        }
        #endregion

        #region PrivateMethod(s)
        #endregion
    }
}
