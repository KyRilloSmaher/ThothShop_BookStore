
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;

namespace ThothShop.Core.Features.Users.Commands.Models
{
    public class DeleteUserCommandModel : IRequest<Response<string>>
    {
        public DeleteUserCommandModel(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }   
   
}
