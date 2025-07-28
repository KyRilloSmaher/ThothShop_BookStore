using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Authors.Queries.Responses;

namespace ThothShop.Core.Features.Authors.Queries.Models
{
    public class GetAuthorByIdQueryModel:IRequest<Response<GetAuthorResponse>>
    {
        [Required]
        public Guid Id { get; set; }
        public GetAuthorByIdQueryModel(Guid id)
        {
            Id = id;
        }
    }
}
