﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Helpers
{
    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PubKey { get; set; }
    }
}
