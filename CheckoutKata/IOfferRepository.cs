namespace CheckoutKata
{
    public interface IOfferRepository
    {
        Offer GetOfferBySKUAndQuantity(string sKU, int quantity);
    }
}