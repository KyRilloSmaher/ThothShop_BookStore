using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Categories.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Categories
{
    public partial class CategoryProfile
    {
        public void GetCategoryQueryMapping() {
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.Icon.Url));
        }
    }
}
