using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    public interface ICheckout
    {
        int ScanInNoSpecials(IEnumerable<string> shoppingCart);
        int ScanInWithSpecials(IEnumerable<string> shoppingCart);
    }
}
