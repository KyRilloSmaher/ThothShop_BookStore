﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Service.Commans
{
    public class EditRoleRequest
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
    }
}
