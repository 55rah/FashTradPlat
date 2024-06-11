using System.ComponentModel.DataAnnotations;

namespace FashTradPlat.Models
{
    public class Checkout
    {
        [Key] public int Checkout_ID { get; set; }

        public required decimal Price { get; set; }
        public int Transaction_ID { get; set; }
        public required Transaction Transaction { get; set; }
    }
}
