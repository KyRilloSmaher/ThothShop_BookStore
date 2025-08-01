﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Core.Features.Users.Queries.Responses
{
    public class UserResponse
    {
        public UserResponse() { }
        public string Id { set; get; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
