using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashTradPlat.Models
{
    public class Checkout
    {
        [Key] public int Checkout_ID { get; set; }
        [ForeignKey("Product")] public int? Product_ID { get; set; } // Foreign Key
        public Product? Product { get; set; } // Reference Navigation property
        public required decimal Price { get; set; }
    }
}
