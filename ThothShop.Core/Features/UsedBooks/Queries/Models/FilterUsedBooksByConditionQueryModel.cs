using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Core.Bases;
using ThothShop.Core.Features.UsedBooks.Queries.Responses;
using ThothShop.Domain.Enums;

namespace ThothShop.Core.Features.UsedBooks.Queries.Models
{
    public class FilterUsedBooksByConditionQueryModel:IRequest<Response<IEnumerable<GetUsedBookResponse>>>
    {
        public UsedBookCondition UsedBookCondition { get; set; }

        public FilterUsedBooksByConditionQueryModel(UsedBookCondition usedBookCondition)
        {
            UsedBookCondition = usedBookCondition;
        }
    }
}
