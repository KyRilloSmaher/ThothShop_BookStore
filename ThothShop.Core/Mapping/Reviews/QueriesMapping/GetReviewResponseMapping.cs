using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Reviews.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Reviews
{
    public partial class ReviewProfile
    {
        public void  GetReviewResponseMapping()
        {
            CreateMap<Review ,ReviewResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest=>dest.Rating ,opt=>opt.MapFrom(src=>src.Rating));
        }
    }
     
}
