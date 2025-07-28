using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Mapping.Users
{
    public partial class UserProfile :Profile
    {
        public UserProfile() {

            GetUserResponseMapping();
            CreateUserCommandModelMapping();
            UpdateuserCommandModelMapping();

        }
    }
}
