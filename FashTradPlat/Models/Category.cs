namespace FashTradPlat.Models
{
    public class Category
    {
        public int Category_ID { get; set; }
        public required string Category_Name { get; set; }
        public required ICollection<Product> Products { get; set; }
    }
}
