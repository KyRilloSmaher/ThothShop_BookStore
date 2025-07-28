using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Books.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Books
{
    public partial class BookProfile
    {
       public void GetBookResponseQueryMapping() {
            CreateMap<Book, GetBookResponse>().ForMember(b=>b.CategoryName ,opt =>opt.MapFrom(src =>src.Category.Name))
                .ForMember(dest => dest.primaryImage, opt => opt.MapFrom(src => src.BookImages.FirstOrDefault(BI => BI.Image.IsPrimary == true).Image.Url))
                .ForMember(dest => dest.BookImagesUrls, opt => opt.MapFrom(src => src.BookImages.Select(i => i.Image.Url)));
        }
    }
}
