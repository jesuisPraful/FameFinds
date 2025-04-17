using DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FameFindsAPI.Models
{
    public class Product
    {
        
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        [ForeignKey(nameof(Category.CategoryId))]
        public int? CategoryId { get; set; }
    }
}
