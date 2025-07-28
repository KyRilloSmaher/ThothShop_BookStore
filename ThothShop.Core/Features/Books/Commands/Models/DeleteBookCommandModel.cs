using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Books.Commands.Models
{
    public class DeleteBookCommandModel : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public DeleteBookCommandModel(Guid id)
        {
            this.Id = id;
        }
    }
}
