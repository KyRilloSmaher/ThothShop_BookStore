using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Reviews.Commands.Models;
using ThothShop.Core.Features.Reviews.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Reviews
{
    
        public partial class ReviewProfile
        {
            public void CreateReviewCommandMapping()
            {
            CreateMap<CreateReviewCommandModel, Review>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));
                   
            }
        }
    
}
