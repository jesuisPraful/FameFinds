using System.ComponentModel.DataAnnotations;

namespace FameFindsWebServices.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [MaxLength(100)]
        public string? CategoryName { get; set; }
    }
}
