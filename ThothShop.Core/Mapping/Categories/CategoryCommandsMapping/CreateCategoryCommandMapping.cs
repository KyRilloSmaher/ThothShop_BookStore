using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Categories.Commands.Models;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Categories
{
    public partial class CategoryProfile
    {
        public void CreateCategoryCommandMapping() {
            CreateMap<CreateCategoryCommandModel, Category>();
        }
    }
}
