namespace FashTradPlat.Models
{
    public class Checkout
    {
        public int Checkout_ID { get; set; }
        public int Product_ID { get; set; }
        public required decimal Price { get; set; }
        public required Product Product { get; set; }
        public required Transaction Transaction { get; set; }
    }
}
