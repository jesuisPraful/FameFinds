
namespace FameFindsDAL.DTOs
{
    public class RatingDto
    {
        public string CustomerName { get; set; }
        public double RatingValue { get; set; }
        public string Review { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
