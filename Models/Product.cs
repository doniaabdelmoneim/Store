using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Store.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Brand { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Precision(16,2)]
        public decimal Price { get; set; }

        public string ImageFileName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // Additional properties can be added as needed
    }
}
