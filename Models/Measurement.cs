using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tailor_shop.Models
{
    public class Measurement
    {
        [Key]
        public int MeasurementId { get; set; }  // Primary Key

        [Required]
        public int CustomerId { get; set; }  // Foreign Key to Customer

        [Required]
        public int ProductId { get; set; }  // Foreign Key to Product (if necessary)

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Neck { get; set; }  // Example of a body measurement

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Chest { get; set; }  // Example of a body measurement

        [Column(TypeName = "decimal(18,2)")]
        public decimal Waist { get; set; }  // Example of a body measurement

        [Column(TypeName = "decimal(18,2)")]
        public decimal Hips { get; set; }  // Example of a body measurement

        [Column(TypeName = "decimal(18,2)")]
        public decimal Inseam { get; set; }  // Example of a body measurement

        // Navigation properties for related Customer and Product
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
