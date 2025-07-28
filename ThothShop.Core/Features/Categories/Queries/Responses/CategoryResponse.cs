﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Features.Categories.Queries.Responses
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IconUrl { get; set; }
    }
}
