using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Mapping.Authors
{
    public partial class AuthorProfile :Profile
    {
        public AuthorProfile() {

            GetAuthorMapping();
            GetAllAuthorPaginatedMapping();
            CreateAuthorMapping();
            UpdateAuthorMapping();
        }
    }
}
