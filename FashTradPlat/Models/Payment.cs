using System.ComponentModel.DataAnnotations;

namespace FashTradPlat.Models
{
    public class Payment
    {
        [Key] public int Payment_ID { get; set; }
        public required decimal Payment_amount { get; set; }
        public required string Payment_method { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
