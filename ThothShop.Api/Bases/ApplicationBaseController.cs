﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ThothShop.Core.Bases;

namespace ThothShop.Api.Bases
{
    [ApiController]
    public class ApplicationBaseController : ControllerBase
    {
            private IMediator _mediatorInstance;
            protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

            #region Actions
            public ObjectResult FinalResponse<T>(Response<T> response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return new OkObjectResult(response);
                    case HttpStatusCode.Created:
                        return new CreatedResult(string.Empty, response);
                    case HttpStatusCode.Unauthorized:
                        return new UnauthorizedObjectResult(response);
                    case HttpStatusCode.BadRequest:
                        return new BadRequestObjectResult(response);
                    case HttpStatusCode.NotFound:
                        return new NotFoundObjectResult(response);
                    case HttpStatusCode.Accepted:
                        return new AcceptedResult(string.Empty, response);
                    case HttpStatusCode.UnprocessableEntity:
                        return new UnprocessableEntityObjectResult(response);
                    default:
                        return new BadRequestObjectResult(response);
                }
            }
            #endregion
        }
    }
