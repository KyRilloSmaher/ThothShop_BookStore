
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.UsedBooks.Commands.Models
{
    public class DeleteUsedBookCommandModel:IRequest<Response<string>>
    {
        public Guid Id { get; set; }

        public DeleteUsedBookCommandModel(Guid id)
        {
            Id = id;
        }
    }
}
