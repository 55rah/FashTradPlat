using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashTradPlat.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; } // Foreign Key
        public required string Image { get; set; }
        [Display(Name = "Product Name")]
        public required string Product_name { get; set;}
        [Display(Name = "Description")]
        public required string Product_description { get; set;}
        [Display(Name = "Requested Product")]
        public required string Product_request { get; set;}
        public Category Category { get; set; } // Reference Navigation property
    }
}
