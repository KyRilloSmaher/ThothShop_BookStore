using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Mapping.Reviews
{
    public partial class ReviewProfile : Profile
    {
        public ReviewProfile() {
            GetReviewResponseMapping();
            UpdateReviewCommandMapping();
            CreateReviewCommandMapping();
        }
    }
}
