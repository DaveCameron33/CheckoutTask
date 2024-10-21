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
        public void ShouldReturnDiscountedItemsPrice1()
        {
            //	Arrange
            IEnumerable<string> shoppingCart = new[] { "A" };

            //	Act
            int shoppingCartTotal = _processor.ScanInWithSpecials(shoppingCart);

            //	Assert
            Assert.NotNull(shoppingCartTotal);
            Assert.Equal(50, shoppingCartTotal);
        }

        /// 1xA=50
        [InlineData(50, new[] { "A" })]

        /// 3xA=130
        [InlineData(130, new[] { "A", "A", "A" })]

        /// 2xB=45; 1xA=50: 45+50=95
        [InlineData(95, new[] { "B", "A", "B" })]

        /// 1xA=50; 1xB=30; 1xC=20; 1xD=15: 50+30+20+15=115
        [InlineData(115, new[] { "A", "B", "C", "D" })]

        /// 4xA=180 (3xA=130; 1xA=50: 130+50=180); 4xB=90 (2xB=45; 2xB=45: 45+45=90) 2xC=40; 1xD=15: 180+90+40+15=325
        [InlineData(325, new[] { "A", "B","A", "C", "B", "D", "B", "C", "A", "B", "A" })]    
        [Theory]
        public void ShouldReturnDiscountedItemsPrice(int total, IEnumerable<string> shoppingCart)
        {
            //	Arrange
            //	Act
            int shoppingCartTotal = _processor.ScanInWithSpecials(shoppingCart);

            //	Assert
            Assert.NotNull(shoppingCartTotal);
            Assert.Equal(total, shoppingCartTotal);
        }
    }
}