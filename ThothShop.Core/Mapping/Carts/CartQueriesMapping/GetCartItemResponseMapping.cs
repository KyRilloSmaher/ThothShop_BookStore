using AutoMapper;
using ThothShop.Core.Features.Carts.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Carts
{
    public partial class CartProfile
    {
        public void GetCartItemReponseMapping()
        {
            CreateMap<CartItem, CartItemResponse>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CartId))
             .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.BookId))
             .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : string.Empty))
             .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
             .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
} 