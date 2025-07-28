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
    public interface IBookImageRepository : IGenericRepository<BookImages>
    {
        public Task<IEnumerable<BookImages>> GetAllImagesByBookAsync(Guid bookId);
        public Task<BookImages> GetPrimaryImageForBookAsync(Guid bookId);
    }
}
