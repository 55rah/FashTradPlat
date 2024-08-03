using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashTradPlat.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; } // Foreign Key from category table
        [Required]
        public required string Image { get; set; }
        // Required attribute ensures a value inputted into the database
        // Display name attribute changes the display name on the web app
        // Regular expression is a client side validation, these regular expressions only allows 0-9, A-z, spaces and any commonly used foreign characters
        [Required]
        [Display(Name = "Product Name")]
        [RegularExpression("^([ \u00c0-\u01ffa-zA-Z'0-9]{1,50})$", ErrorMessage = "Please enter a valid name")]
        public required string Product_name { get; set;}
        [Required]
        [Display(Name = "Description")]
        [RegularExpression("^([ À-ǿa-zA-Z'0-9]{1,70})$", ErrorMessage = "Please enter a valid description")] 
        public required string Product_description { get; set;}
        [Required]
        [Display(Name = "Requested Product")]
        [RegularExpression("^([ \u00c0-\u01ffa-zA-Z'0-9]{1,50})$", ErrorMessage = "Please enter a valid name")]
        public required string Product_request { get; set;}
        public Category Category { get; set; } // Reference Navigation property establishes foreign key from category table
    }
}
