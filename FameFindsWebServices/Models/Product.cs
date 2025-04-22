using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FameFindsWebServices.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [MaxLength(150)]
        public string? ProductName { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
    }
}
