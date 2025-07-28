using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Books.Queries.Responses;

namespace ThothShop.Core.Mapping.Books
{
    public partial class BookProfile :Profile
    {
        public BookProfile() { 
          
            GetBookResponseQueryMapping();
            GetBookDashboardResponseMapping();
            CreateBookCommandModelMapping();
            UpdateBookCommandModelMapping();
        }
    }
}
