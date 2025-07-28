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
        private void GetBookDashboardResponseMapping()
        {
            CreateMap<Book,GetBookResponseForDashboard>()
                .ForMember(b => b.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock));
        }
    }
   
}
