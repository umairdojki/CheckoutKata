using Moq;
using NUnit.Framework;

namespace CheckoutKata.Tests
{
    public class CheckoutTests
    {
        private Mock<IItemRepository> _itemRepository;
        private Mock<IOfferRepository> _offerRepository;
        private Checkout _checkout;

        [SetUp]
        public void Setup()
        {
            _itemRepository = new Mock<IItemRepository>();
            _offerRepository = new Mock<IOfferRepository>();
            _checkout = new Checkout(_itemRepository.Object, _offerRepository.Object);
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

        [Test]
        public void TotalPrice_correctly_calcultates_the_total_of_all_offer_and_nonoffer_items()
        {
            // given
            var item1 = new Item("Item1", 1.99M);
            var item2 = new Item("Item2", 2.99M);
            var offerOnItem1 = new Offer("Item1", 2.49M, 2);
            _itemRepository.Setup(x => x.GetItemBySKU("Item1")).Returns(item1);
            _itemRepository.Setup(x => x.GetItemBySKU("Item2")).Returns(item2);
            _offerRepository.Setup(x => x.GetOfferBySKUAndQuantity("Item1", 2)).Returns(offerOnItem1);

            // when
            _checkout.ScanItem("Item1");
            _checkout.ScanItem("Item2");
            _checkout.ScanItem("Item1");

            // then
            var expected = offerOnItem1.OfferPrice + item2.UnitPrice;
            Assert.AreEqual(expected, _checkout.TotalPrice);
        }

        [Test]
        public void TotalPrice_picks_better_offer_when_multiple_offers_on_same_item()
        {
            // given
            var item = new Item("Item1", 1.99M);
            var offer1 = new Offer("Item1", 2.49M, 2);
            var offer2 = new Offer("Item1", 2.99M, 3);
            _itemRepository.Setup(x => x.GetItemBySKU("Item1")).Returns(item);
            _offerRepository.Setup(x => x.GetOfferBySKUAndQuantity("Item1", 2)).Returns(offer1);
            _offerRepository.Setup(x => x.GetOfferBySKUAndQuantity("Item1", 3)).Returns(offer2);

            // when
            _checkout.ScanItem("Item1");
            _checkout.ScanItem("Item1");
            _checkout.ScanItem("Item1");

            // then
            Assert.AreEqual(offer2.OfferPrice, _checkout.TotalPrice);
        }
    }
}