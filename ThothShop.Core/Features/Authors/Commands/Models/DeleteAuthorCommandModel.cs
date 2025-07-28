using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Authors.Commands.Models
{
    public class DeleteAuthorCommandModel:IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public DeleteAuthorCommandModel(Guid id)
        {
            Id = id;
        }
    }
}
