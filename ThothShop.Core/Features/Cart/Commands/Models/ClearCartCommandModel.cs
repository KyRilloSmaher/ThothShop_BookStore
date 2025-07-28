using MediatR;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Carts.Commands.Models
{
    public class ClearCartCommandModel : IRequest<Response<string>>
    {
        public ClearCartCommandModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
} 