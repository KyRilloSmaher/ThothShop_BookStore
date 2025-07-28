using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.WishLists.Commands.Models
{
    public class ClearWishlistCommnandModel : IRequest<Response<string>>
    {
        public string UserId { get; private set; }

        public ClearWishlistCommnandModel(string userId)
        {
            UserId = userId;
        }
    }
}
