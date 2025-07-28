using MediatR;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Carts.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.Carts.Queries.Models
{
    public class GetUserCartsQueryModel : IRequest<Response<IEnumerable<Cartresponse>>>
    {
        public GetUserCartsQueryModel(string userId)
        {
            UserId = userId;
        }

        public string UserId{get; set;}
    }
} 