using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Features.Users.Commands.Models;
using ThothShop.Core.Features.Users.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void CreateUserCommandModelMapping() {
            CreateMap<CreateUserCommandModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
