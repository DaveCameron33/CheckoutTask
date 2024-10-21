namespace Checkout
{
    public class Checkout:ICheckout
    {
        List<StoreItem> storeItems;

        public Checkout(StoreItems _storeItems)
        {
            storeItems = _storeItems.StockItems;
        }

        Dictionary<string, int> cartItems = new Dictionary<string, int>();

        private void Scan(IEnumerable<string> shoppingCart)
        {
            foreach (string cartItem in shoppingCart)
            {
                if (!cartItems.ContainsKey(cartItem))
                {
                    cartItems.Add(cartItem, 0);
                }
                cartItems[cartItem]++;
            }
        }
        public int ScanInNoSpecials(IEnumerable<string> shoppingCart)
        {
            int subTotal = 0;

            Scan(shoppingCart);

            foreach (var cartItem in cartItems)
            {
                var units = cartItem.Value;
                StoreItem storeItem = storeItems.Where(i => i.SKU == cartItem.Key.ToString()).First();
                var unitPrice = storeItem.UnitPrice;
                subTotal += (units * unitPrice);
            }

            return subTotal;
        }

        public int ScanInWithSpecials(IEnumerable<string> shoppingCart)
        {
            int subTotal = 0;

            Scan(shoppingCart);

            foreach (var cartItem in cartItems)
            {
                var units = cartItem.Value;
                StoreItem storeItem = storeItems.Where(i => i.SKU == cartItem.Key.ToString()).First();

                if (storeItem.SpecialPrice?.Qty > 0)
                {
                    var atThisPrice = units / storeItem.SpecialPrice.Qty;
                    subTotal += atThisPrice * storeItem.SpecialPrice.Price;

                    atThisPrice = units % storeItem.SpecialPrice.Qty;
                    subTotal += atThisPrice * storeItem.UnitPrice;
                }
                else
                {
                    subTotal += (units * storeItem.UnitPrice);
                }
            }
            return subTotal;
        }

    }
}
