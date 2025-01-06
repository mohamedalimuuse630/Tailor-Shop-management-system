using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tailor_shop.ViewModel
{
    public class ProductViewModel
    {
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
    }
}
