using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.Contracts
{
    public interface IAuthorImageRepository : IGenericRepository<AuthorImages>
    {
        public Task<IEnumerable<AuthorImages>> GetAllImagesByAuthorAsync(Guid bookId);
        public Task<AuthorImages> GetPrimaryImageForAuthorAsync(Guid bookId);
    }
}
