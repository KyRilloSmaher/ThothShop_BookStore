using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Books.Commands.Models;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Books
{
    public partial class BookProfile
    {
        public void CreateBookCommandModelMapping() {
            CreateMap<CreateBookCommandModel, Book>()
                .ForMember(dest => dest.BookImages, opt => opt.Ignore());
        }
    }
}
