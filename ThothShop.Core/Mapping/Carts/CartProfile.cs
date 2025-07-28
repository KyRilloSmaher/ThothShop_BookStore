using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Carts.Commands.Models;
using ThothShop.Core.Features.Carts.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Carts
{
    public partial class CartProfile : Profile
    {
        public CartProfile()
        {
            GetCartResponseMapping();
            GetCartItemReponseMapping();
            CreateCartMapping();

        }
    }
}
