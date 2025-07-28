using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Books.Queries.Responses;

namespace ThothShop.Core.Features.Books.Queries.Models
{
    public class GetBookByIdQueryModel : IRequest<Response<GetBookResponse>>
    {
        public GetBookByIdQueryModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
