using AutoMapper;
using ThothShop.Core.Features.Carts.Commands.Models;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Carts
{
    public partial class CartProfile
    {
        public void CreateCartMapping()
        {
            CreateMap<CreateCartCommandModel, Cart>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.ToString()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => new List<CartItem>()))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => 0m));
        }
    }
} 