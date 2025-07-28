using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.UsedBooks.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.UsedBooks
{
    public partial class UsedBookProfile
    {
        public void GetUsedBookResponseMapping() {
            CreateMap<UsedBook, GetUsedBookResponse>()
                  .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.user.UserName))
                  .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                  .ForMember(dest => dest.primaryImage, opt => opt.MapFrom(src => src.BookImages.FirstOrDefault(BI => BI.Image.IsPrimary == true).Image.Url))
                  .ForMember(dest => dest.BookImages, opt => opt.MapFrom(src => src.BookImages.Select(BI => BI.Image.Url).ToList()));


        }
    }
}
