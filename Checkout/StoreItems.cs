using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    public class StoreItems
    {
        public List<StoreItem> StockItems { get; set; }
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
