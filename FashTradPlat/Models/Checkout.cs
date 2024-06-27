using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashTradPlat.Models
{
    public class Checkout
    {
        [Key] public int Checkout_ID { get; set; }
        [ForeignKey("Product")] public int? Product_ID { get; set; } // Foreign Key
        public Product? Product { get; set; } // Reference Navigation property

        // This [Column(TypeName = "decimal(7,2)")] defines the decimal field for Price where 7 is the allowed amount of total digits in a number and 2 is the amont after a decimal
        [Column(TypeName = "decimal(7,2)")]
        public required decimal Price { get; set; }
    }
}
