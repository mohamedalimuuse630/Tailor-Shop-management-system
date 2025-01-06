using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tailor_shop.ViewModel
{
    public class PaymentVeiwModel
    {
        public int PaymentId { get; set; }  // Primary Key

        [Required]
        public int OrderId { get; set; }  // Foreign Key to Order

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }  // Amount paid for the order

        public DateTime PaymentDate { get; set; }  // Date of payment

        [Required]
        public string PaymentMethod { get; set; }
    }
}
