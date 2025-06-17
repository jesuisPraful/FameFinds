using System.ComponentModel.DataAnnotations;

namespace FameFindsWebServices.Models
{
    public class ShopProduct
    {
        public int ShopProductId { get; set; }
        [Required]
        public int? ShopId { get; set; }
        [Required]
        public int? ProductId { get; set; }

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

    }
}
