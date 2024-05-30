namespace FashTradPlat.Models
{
    public class Transaction
    {
        public int Transaction_ID { get; set; }
        public int Product_ID { get; set; }
        public int Payment_ID {  get; set; }
        public int Checkout_ID { get; set; }
        public DateOnly TransactionDate { get; set; }
        public string? SendAddress { get; set; }
        public DateOnly EstCompletion { get; set; }

        public required Checkout Checkout { get; set; }
        public required Product Product { get; set; }
        public required Payment Payment { get; set; }


    }
}
