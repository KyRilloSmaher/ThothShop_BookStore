using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Authentications.Commands.Models;
using ThothShop.Domain.Helpers;
using ThothShop.Service.Commans;
using ThothShop.Service.Contract;
using System.Security.Claims;

namespace ThothShop.Core.Features.Authentications.Commands.CommandsHandlers
{
    public class CommandHandler : ResponseHandler
                                  , IRequestHandler<SendResetCodeCommandModel, Response<string>>
                                  , IRequestHandler<ResetPasswordCommandModel, Response<string>>
                                  , IRequestHandler<ChangePasswordCommandModel, Response<string>>
                                  , IRequestHandler<RefreshTokenCommandModel, Response<JwtResponse>>
    {
        #region Field(s)
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor(s)
        public CommandHandler(IAuthenticationService authenticationService, IMapper mapper, IUserService userService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userService = userService;
        }

        #endregion

        #region HandleMethod(s)
        public async  Task<Response<string>> Handle(SendResetCodeCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);
            switch(result)
            {
                case SystemMessages.NotFound:
                    return BadRequest<string>(string.Format(SystemMessages.EntityNotFound,"User"));
                case SystemMessages.Failed:
                    return BadRequest<string>(string.Format(SystemMessages.Failed, "Send Reset Code"));
                case SystemMessages.Success:
                    break;

            }
            return Success(result);
        }

        public async Task<Response<string>> Handle(ResetPasswordCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPassword(request.Email,request.newPassword);
            switch (result)
            {
                case SystemMessages.NotFound:
                    return BadRequest<string>(string.Format(SystemMessages.EntityNotFound, "User"));
                case SystemMessages.Failed:
                    return BadRequest<string>(string.Format(SystemMessages.Failed, "Reset Password"));
                case SystemMessages.Success:
                    break;
            }
            return Success(result);
        }

        public async  Task<Response<string>> Handle(ChangePasswordCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ChangePasswordAsync(request.Email, request.OldPassword, request.NewPassword);
            switch (result)
            {
                case SystemMessages.NotFound:
                    return BadRequest<string>(string.Format(SystemMessages.EntityNotFound, "User"));
                case SystemMessages.Failed:
                    return BadRequest<string>(string.Format(SystemMessages.Failed, "Change Password"));
                case SystemMessages.Success:
                    break;
            }
            return Success(result);

        }

public async Task<Response<JwtResponse>> Handle(RefreshTokenCommandModel request, CancellationToken cancellationToken)
{
            // Step 1: Decode and validate access token
            var token = _authenticationService.ReadJWTToken(request.AccessToken);
            if (token == null)
                return BadRequest<JwtResponse>("Access token is malformed or invalid.");

            if (token.ValidTo > DateTime.UtcNow)
                return BadRequest<JwtResponse>("Access token is still valid. Refresh not required.");

            // Step 2: Validate refresh token
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return BadRequest<JwtResponse>("Refresh token must be provided.");

            // Step 3: Extract user ID from token
            var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<JwtResponse>("User ID claim is missing from access token.");

            // Step 4: Check if user exists
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return BadRequest<JwtResponse>("No user found for the provided user ID.");

            // Step 5: Validate token and refresh token pair
            (string validatedUserId, DateTime? refreshExpiry) = await _authenticationService.ValidateDetails(
                token,
                request.AccessToken,
                request.RefreshToken
            );

            // Step 6: Ensure user IDs match
            if (validatedUserId != userId)
                return BadRequest<JwtResponse>("Token mismatch: User ID in token does not match stored data.");

            // Step 7: Generate new token pair
            var newTokens = await _authenticationService.RetrieveRefreshToken(
                user,
                token,
                request.RefreshToken,
                refreshExpiry
            );

            // Step 8: Return new tokens
            return Success(newTokens);
        }
        #endregion
    }
}
