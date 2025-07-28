using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Mapping.Categories
{
    public partial class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            GetCategoryQueryMapping();
            CreateCategoryCommandMapping();
            UpdateCategoryCommandMapping();
        }
    }
}
