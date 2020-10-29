using System.Collections.Generic;

namespace CheckoutKata
{
    public class Checkout
    {
        public decimal TotalPrice { get { return CalculateTotalPrice(); } }
        private Dictionary<Item, int> _scannedItems { get; }
        private IItemRepository _itemRepository { get; }

        public Checkout(IItemRepository itemRepository)
        {
            _scannedItems = new Dictionary<Item, int>();
            _itemRepository = itemRepository;
        }

        public void ScanItem(string sku)
        {
            var item = _itemRepository.GetItemBySKU(sku);

            if (!_scannedItems.ContainsKey(item))
                _scannedItems[item] = 0;

            _scannedItems[item]++;
        }

        private decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var (item, quantity) in _scannedItems)
            {
                totalPrice += item.UnitPrice * quantity;
            }

            return totalPrice;
        }
    }
}
