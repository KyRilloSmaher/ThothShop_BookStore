using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Bases;
using ThothShop.Infrastructure.context;
using ThothShop.Infrastructure.RepositoriesContracts;

namespace ThothShop.Infrastructure.RepositoriesImplementions
{
    internal class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly ApplicationDBContext _context;

        public ImageRepository(ApplicationDBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
