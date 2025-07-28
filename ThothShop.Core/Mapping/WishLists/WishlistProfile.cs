using AutoMapper;
using ThothShop.Core.Features.WishLists.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.WishLists
{
    public class WishlistProfile : Profile
    {
        public WishlistProfile()
        {

            CreateMap<Wishlist, WishlistItemResponse>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.Book.Price))
                .ForMember(dest => dest.BookImageUrl, opt => opt.MapFrom(src => src.Book.BookImages.FirstOrDefault(i => i.Image.IsPrimary == true).Image.Url))
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
} 