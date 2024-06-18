using System.ComponentModel.DataAnnotations;

namespace FashTradPlat.Models
{
    public class Payment
    {
        [Key] public int Payment_ID { get; set; }
        [Display(Name = "Amount")]
        public required decimal Payment_amount { get; set; }
        [Display(Name = "Method")]
        public required string Payment_method { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }
    }
}
