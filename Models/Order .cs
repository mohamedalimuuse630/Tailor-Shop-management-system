using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TailorShop.Models;

namespace Tailor_shop.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; } // Primary Key

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; } // Foreign Key to Customer

        [Required]
        public DateTime OrderDate { get; set; } // Date when the order was placed

        public DateTime? DeliveryDate { get; set; } // Date when the order is expected or delivered (nullable)

        [Required]
        [StringLength(50)]
        public string Status { get; set; } // Order status (e.g., Pending, Completed)

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } // Total cost of the order

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; } // Payment status (e.g., Paid, Pending)

        // Navigation property to related Customer
        public Customer Customer { get; set; }

        // Navigation property for related OrderItems
        //public ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
       
    }
}
