using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [Display(Name = "Transaction Date")]
        // The [DataType(DataType.Date)] annotation makes the field a date field where a user can select a date from a calender GUI. 
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }
        public Transaction()
        {
            TransactionDate = DateTime.Now;
        }

        [Display(Name = "Recieving Address")]
        public string? SendAddress { get; set; }
        [Display(Name = "Estimated Trade Completion")]
        public DateTime EstCompletion { get; set; }


    }
}
