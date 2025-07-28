using System;

namespace ThothShop.Core.Features.WishLists.Queries.Responses
{
    public class WishlistItemResponse
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public decimal BookPrice { get; set; }
        public string BookImageUrl { get; set; }
        public DateTime AddedAt { get; set; }
    }
} 