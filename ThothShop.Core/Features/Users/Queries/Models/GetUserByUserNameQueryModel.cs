using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Users.Queries.Responses;

namespace ThothShop.Core.Features.Users.Queries.Models
{
    public class GetUserByUserNameQueryModel : IRequest<Response<UserResponse>>
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        public GetUserByUserNameQueryModel(string userName)
        {
            UserName = userName;
        }
      
    }
}
