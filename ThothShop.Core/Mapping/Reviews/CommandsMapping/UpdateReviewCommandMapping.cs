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
            public void UpdateReviewCommandMapping()
            {
                CreateMap<UpdateReviewCommandModel,Review>()
           
                    .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                    .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
            }
        }
    
}
