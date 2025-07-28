using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ThothShop.Core.Features.Carts.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Carts
{
    public partial class CartProfile
    {
        public void GetCartResponseMapping()
        {
            CreateMap<Cart, Cartresponse>()  // Fixed typo in CartResponse
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.Items.Sum(i => i.Quantity)))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Items));
        }
    }
}
