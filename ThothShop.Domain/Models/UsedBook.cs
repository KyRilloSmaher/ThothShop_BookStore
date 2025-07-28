using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Enums;

namespace ThothShop.Domain.Models
{
    public class UsedBook : BookBase
    {

        public string UserId { get; set; }
        public UsedBookCondition Condition { get; set; }
        public string Comment { get; set; }
        public string Note_WayOfConnect { get; set; }

        public User user { get; set; }
    }
}
