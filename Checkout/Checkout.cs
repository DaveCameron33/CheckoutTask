namespace Checkout
{
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
}
