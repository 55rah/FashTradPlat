using System.ComponentModel.DataAnnotations;

namespace FashTradPlat.Models
{
    public class Category
    {
        [Key] public int Category_ID { get; set; }
        [Display(Name = "Category Name")]
        [RegularExpression("^[a-zA-Z]{1,20}$", ErrorMessage = "Please enter a valid category name")]
        public string Category_Name { get; set; }
        public  ICollection<Product> Products { get; set; }
    }
}
