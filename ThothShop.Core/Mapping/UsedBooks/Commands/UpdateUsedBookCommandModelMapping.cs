using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.UsedBooks.Commands.Models;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.UsedBooks
{
    public partial class UsedBookProfile
    {
        public void UpdateUsedBookCommandModelMapping()
        {
            CreateMap<UpdateUsedBookCommandModel, UsedBook>()
             .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src =>src.CategoryId))
             .ForMember(dest => dest.BookImages, opt => opt.Ignore())
             .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.ViewCount, opt => opt.Ignore())
             .ForMember(dest => dest.Category, opt => opt.Ignore())
             .ForMember(dest => dest.user, opt => opt.Ignore())
             .ForMember(dest => dest.Authors, opt => opt.Ignore());


        }
    }
}
