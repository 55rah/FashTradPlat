using System.ComponentModel.DataAnnotations;

namespace FashTradPlat.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Category_Name { get; set; }
        public  ICollection<Product> Products { get; set; }
    }
}
