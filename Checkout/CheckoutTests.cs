namespace Checkout
{
    public class CheckoutTests
    {

        private readonly StoreItems storeItems;

        public CheckoutTests()
        {
            storeItems = new StoreItems();
            storeItems.StockItems.Add(new StoreItem("A", 50, new SpecialPrice(3,130)));
            storeItems.StockItems.Add(new StoreItem("B", 30, new SpecialPrice(2, 45)));
            storeItems.StockItems.Add(new StoreItem("C", 20));
            storeItems.StockItems.Add(new StoreItem("D", 15));
        }

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

    public class Checkout
    {
        List<StoreItem> storeItems;
        public Checkout(StoreItems _storeItems)
        {
            storeItems = _storeItems.StockItems;
        }

        Dictionary<string, int> cartItems = new Dictionary<string, int>();

        public int ScanInNoSpecials(IEnumerable<string> shoppingCart)
        {
            int subTotal = 0;

            foreach (string cartItem in shoppingCart)
            {
                if (!cartItems.ContainsKey(cartItem))
                {
                    cartItems.Add(cartItem, 0);
                }
                cartItems[cartItem]++;
            }

            foreach (var cartItem in cartItems)
            {
                var units = cartItem.Value;
                StoreItem storeItem = storeItems.Where(i => i.SKU == cartItem.Key.ToString()).First();
                var unitPrice = storeItem.UnitPrice;
                subTotal += (units * unitPrice);
            }

            return subTotal;
        }
    }
    public class StoreItems
    {
        public List<StoreItem> StockItems {  get; set; }
        public StoreItems()
        {
            StockItems = new List<StoreItem>();
        }
    }
    public class StoreItem
    {
        public string SKU { get; set; }
        public int UnitPrice { get; set; }
        public SpecialPrice SpecialPrice { get; set; }

        public StoreItem(string sku, int unitPrice, SpecialPrice specialPrice = null)
        {
            SKU = sku;
            UnitPrice = unitPrice;
            SpecialPrice = specialPrice;
        }
    }
    public class SpecialPrice
    {
        public int Qty { get; set; }
        public int Price { get; set; }

        public SpecialPrice(int qty, int price)
        {
            Qty = qty;
            Price = price;
        }
    }

}