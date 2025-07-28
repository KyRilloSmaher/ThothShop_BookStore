using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Categories.Commands.Models
{
    public class DeleteCategoryCommandModel: IRequest<Response<string>>
    {
        public Guid Id { get; set; }

        public DeleteCategoryCommandModel(Guid id)
        {
            Id = id;
        }
    }
    
}
