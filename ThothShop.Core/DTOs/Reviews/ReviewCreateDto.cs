namespace ThothShop.Core.Reviews.DTOS
{
    public class CreateReviewDTO
    {
        public Guid bookId { get; set; }
        public string Comment { get; set; }
        public double rating { get; set; }
    }
}