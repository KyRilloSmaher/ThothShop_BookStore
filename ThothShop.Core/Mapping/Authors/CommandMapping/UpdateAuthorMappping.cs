using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Authors.Commands.Models;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Authors
{
    public partial class AuthorProfile
    {
       public void  UpdateAuthorMapping(){
            CreateMap<UpdateAuthorCommandModel, Author>();
        }
    }
}
