namespace CheckoutKata
{
    public class Item
    {
        public string SKU { get; }
        public decimal UnitPrice { get; }

        public Item(string sku, decimal price)
        {
            SKU = sku;
            UnitPrice = price;
        }

        public override int GetHashCode()
        {
            return SKU.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Item)obj;
            return SKU == other.SKU;
        }
    }
}