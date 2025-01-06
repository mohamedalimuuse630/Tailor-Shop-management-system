using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tailor_shop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }  // Primary Key

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public string Size { get; set; }  // Optional

        public string Color { get; set; }  // Optional

        // Navigation property for related OrderItems
        public ICollection<Measurement> Measurements { get; set; }

    }
}
