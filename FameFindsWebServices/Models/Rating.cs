using System.ComponentModel.DataAnnotations;

namespace FameFindsWebServices.Models
{
    public class Rating
    {
        [Key]
        public int? RatingId { get; set; }
        [Required]
        public int? CustomerId { get; set; }
        [Required]
        public int? ShopId { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? RatingValue { get; set; }
        [MaxLength(500)]
        public string? Review { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
