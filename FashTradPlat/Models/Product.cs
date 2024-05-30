namespace FashTradPlat.Models
{
    public class Product
    {
        public int Product_ID { get; set; }
        public int Category_ID { get; set; }
        public required string Image { get; set; }
        public required string Product_name { get; set;}
        public required string Product_description { get; set;} 
        public required string Product_request { get; set;}
        public required Category Category { get; set;}
        public required Checkout Checkout { get; set;}
        public required Transaction Transaction { get; set;}
    }
}
