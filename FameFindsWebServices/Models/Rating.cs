namespace FameFindsWebServices.Models
{
    public class Rating
    {
        public int RatingId { get; set; }

        public int? CustomerId { get; set; }

        public int? ShopId { get; set; }

        public int? RatingValue { get; set; }

        public string? Review { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
