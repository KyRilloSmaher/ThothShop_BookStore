using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.Categories.Queries.Responses;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.Categories.Queries.Models
{
    public class GetCountOFBooksInCategoryQueryModel : IRequest<Response<int>>
    {
        [Required]
        public Guid Id { get; set; }
        public GetCountOFBooksInCategoryQueryModel(Guid id)
        {
            Id = id;
        }
    }
}
