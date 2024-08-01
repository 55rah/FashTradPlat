using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashTradPlat.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; } // Foreign Key
        [Required]
        public required string Image { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        [RegularExpression("^([ \u00c0-\u01ffa-zA-Z'0-9]{1,50})$", ErrorMessage = "Please enter a valid name")]
        public required string Product_name { get; set;}
        [Required]
        [Display(Name = "Description")]
        public required string Product_description { get; set;}
        [Required]
        [Display(Name = "Requested Product")]
        public required string Product_request { get; set;}
        public Category Category { get; set; } // Reference Navigation property
    }
}
