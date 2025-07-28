using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;

namespace ThothShop.Service.Commans
{
    public class SalesReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public Dictionary<OrderStatus, int> OrdersByStatus { get; set; }
        public Dictionary<Guid, int> PopularItems { get; set; }
    }
}
