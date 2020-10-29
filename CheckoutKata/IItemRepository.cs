namespace CheckoutKata
{
    public interface IItemRepository
    {
        Item GetItemBySKU(string sku);
    }
}