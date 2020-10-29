using Moq;
using NUnit.Framework;

namespace CheckoutKata.Tests
{
    public class CheckoutTests
    {
        private Mock<IItemRepository> _itemRepository;
        private Checkout _checkout;

        [SetUp]
        public void Setup()
        {
            _itemRepository = new Mock<IItemRepository>();
            _checkout = new Checkout(_itemRepository.Object);
        }

        [Test]
        public void TotalPrice_correctly_calcultates_the_total_of_all_nonoffer_items()
        {
            // given
            var item1 = new Item("Item1", 1.99M);
            var item2 = new Item("Item2", 2.99M);
            _itemRepository.Setup(x => x.GetItemBySKU("Item1")).Returns(item1);
            _itemRepository.Setup(x => x.GetItemBySKU("Item2")).Returns(item2);

            // when
            _checkout.ScanItem("Item1");
            _checkout.ScanItem("Item2");
            _checkout.ScanItem("Item1");

            // then
            var expected = (item1.UnitPrice * 2) + item2.UnitPrice;
            Assert.AreEqual(expected, _checkout.TotalPrice);
        }
    }
}