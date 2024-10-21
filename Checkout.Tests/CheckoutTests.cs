using Checkout;

namespace Checkout
{
    public class CheckoutTests
    {

        private readonly StoreItems storeItems;
        private readonly Checkout _processor;

        public CheckoutTests()
        {
            storeItems = new StoreItems();
            storeItems.StockItems.Add(new StoreItem("A", 50, new SpecialPrice(3,130)));
            storeItems.StockItems.Add(new StoreItem("B", 30, new SpecialPrice(2, 45)));
            storeItems.StockItems.Add(new StoreItem("C", 20));
            storeItems.StockItems.Add(new StoreItem("D", 15));

            _processor = new Checkout(storeItems);

        }

        [Fact]
        public void ShouldReturnSingleItemPrice()
        {
            //	Arrange
            IEnumerable<string> shoppingCart = new[] { "A" };

            //	Act
            int shoppingCartTotal = _processor.ScanInNoSpecials(shoppingCart);

            //	Assert
            Assert.NotNull(shoppingCartTotal);
            Assert.Equal(50, shoppingCartTotal);
        }
        [Fact]
        public void ShouldReturnDiscountedItemsPrice()
        {
            //	Arrange
            IEnumerable<string> shoppingCart = new[] { "A" };

            //	Act
            int shoppingCartTotal = _processor.ScanInWithSpecials(shoppingCart);

            //	Assert
            Assert.NotNull(shoppingCartTotal);
            Assert.Equal(50, shoppingCartTotal);
        }
    }
}