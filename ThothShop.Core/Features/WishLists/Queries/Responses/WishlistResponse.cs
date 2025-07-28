using System;
using System.Collections.Generic;
using ThothShop.Domain.Models;

namespace ThothShop.Core.Features.WishLists.Queries.Responses
{
    public class WishlistResponse
    {
        public string UserId { get; set; }
        public IEnumerable<WishlistItemResponse> Items { get; set; } = new HashSet<WishlistItemResponse>();
        public int TotalItems { get; set; }
    }
} 