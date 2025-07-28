using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;

namespace ThothShop.Infrastructure.RepositoriesContracts
{
    public interface IImageRepository : IGenericRepository<Image>
    {
    }
}
