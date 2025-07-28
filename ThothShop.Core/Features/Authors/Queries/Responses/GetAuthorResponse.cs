using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;

namespace ThothShop.Core.Features.Authors.Queries.Responses
{
    public class GetAuthorResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public Gender Gender { get; set; }
        public Nationality Nationality { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PrimaryImageUrl { get; set; }
        public IList<string> ImagesUrls { get;set;}
        public IList<string> Categories { get;set;}
    }
}
