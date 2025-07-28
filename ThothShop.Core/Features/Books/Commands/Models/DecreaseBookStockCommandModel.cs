
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Books.Commands.Models
{
    public class DecreaseBookStockCommandModel : IRequest<Response<string>>
    {
        public Guid BookId { get; set; }
        public int amount { get; set; }


    }
}
