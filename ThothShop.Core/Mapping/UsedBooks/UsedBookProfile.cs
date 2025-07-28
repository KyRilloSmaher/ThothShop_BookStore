using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Mapping.UsedBooks
{
    public partial class UsedBookProfile : Profile
    {
        public UsedBookProfile() {

            GetUsedBookResponseMapping();
            CreateUsedBookCommandModelMapping();
            UpdateUsedBookCommandModelMapping();
        }
    }
}
