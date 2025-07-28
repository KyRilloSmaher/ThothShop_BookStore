using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;

namespace ThothShop.Infrastructure.Bases
{
    public class FilterAuthorModel
    {
        public string? SearchTerm { get; set; }
        public Nationality? Nationality { get; set; }
        public FilterAuthorModel()
        { }
            
        public FilterAuthorModel(string searchTerm, Nationality? nationality)
        {
            SearchTerm = searchTerm;
            Nationality = nationality;
        }
    }
}
