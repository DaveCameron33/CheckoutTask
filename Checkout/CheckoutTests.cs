namespace Checkout
{
    public class CheckoutTests
    {
        [Fact]
        public void ShouldReturnSingleItemPrice()
        {
            //	Arrange
            var processor = new Checkout(storeItems);

            IEnumerable<string> shoppingCart = new[] { "A" };

            //	Act
            int shoppingCartTotal = processor.ScanInNoSpecials(shoppingCart);

            //	Assert
            Assert.NotNull(shoppingCartTotal);
            Assert.Equal(50, shoppingCartTotal);
        }
    }
}