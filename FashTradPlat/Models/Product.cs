using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashTradPlat.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; } // Foreign Key

        public required string Image { get; set; }
        public required string Product_name { get; set;}
        public required string Product_description { get; set;} 
        public required string Product_request { get; set;}

        [ForeignKey ("Checkout")] public int CheckoutID { get; set; }
        public required Checkout Checkout { get; set; }
        [ForeignKey("Transaction")] public int Transaction_ID { get; set; }
        public required Transaction Transaction { get; set;}
        public Category Category { get; set; } // Reference Navigation property
    }
}
