using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Authors.Queries.Models.Base;
using ThothShop.Core.Features.Authors.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Authors
{
    public partial class AuthorProfile
    {

        public void GetAllAuthorPaginatedMapping()
        {

            CreateMap<Author, GetAuthorPaginatedListResponse>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.authorCategories.Select(ac => ac.Category.Name)))
                .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.AuthorImages.FirstOrDefault(BI => BI.Image.IsPrimary == true).Image.Url))
                .ForMember(dest => dest.ImagesUrls, opt => opt.MapFrom(src => src.AuthorImages.Select(i => i.Image.Url)));
        }
    }
}
