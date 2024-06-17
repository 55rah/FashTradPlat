using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashTradPlat.Models
{
    public class Transaction
    {
        [Key] public int Transaction_ID { get; set; }
        [ForeignKey("Product")] public int Product_ID { get; set; } // Foreign Key
        public required Product Product { get; set; }  // Reference Navigation property
        [ForeignKey("Payment")] public int Payment_ID {  get; set; } // Foreign Key
        public required Payment Payment { get; set; }  // Reference Navigation property
        [ForeignKey("Checkout")] public int Checkout_ID { get; set; } // Foreign Key
        public required Checkout Checkout { get; set; } // Reference Navigation property
        public DateTime TransactionDate { get; set; }
        public string? SendAddress { get; set; }
        public DateTime EstCompletion { get; set; }


    }
}
