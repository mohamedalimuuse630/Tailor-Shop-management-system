using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tailor_shop.Models;

namespace TailorShop.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }  // Primary Key

        [Required]
        public int OrderId { get; set; }  // Foreign Key to Order

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }  // Amount paid for the order

        public DateTime PaymentDate { get; set; }  // Date of payment

        [Required]
        public string? PaymentMethod { get; set; }  // Payment method (e.g., Cash, Card, Online)

        // Navigation property to Order (foreign key relationship)
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }
}
