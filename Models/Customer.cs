using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tailor_shop.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; } // Primary Key

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]  // Ensure DateOfBirth is required
        public DateTime DateOfBirth { get; set; } // Date of Birth

        // Navigation property for related Orders
        public ICollection<Order> Orders { get; set; }
        public ICollection<Measurement> Measurements { get; set; }
    }
}
