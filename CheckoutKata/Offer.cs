namespace CheckoutKata
{
    public class Offer
    {
        public string SKU { get; }
        public decimal OfferPrice { get; }
        public int Quantity { get; }

        public Offer(string sku, decimal offerPrice, int quantity)
        {
            SKU = sku;
            OfferPrice = offerPrice;
            Quantity = quantity;
        }
    }
}