using System.ComponentModel.DataAnnotations;

namespace FashTradPlat.Models
{
    public class Category
    {
        [Key] public int Category_ID { get; set; }
        public string Category_Name { get; set; }
        public  ICollection<Product> Products { get; set; }
    }
}
