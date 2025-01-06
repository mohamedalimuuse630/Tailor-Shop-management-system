using System.ComponentModel.DataAnnotations;
using Tailor_shop.Models;

namespace Tailor_shop.ViewModel
{
    public class CustomerViewModel
    {
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
        public DateTime DateOfBirth { get; set; } 
    }
}
