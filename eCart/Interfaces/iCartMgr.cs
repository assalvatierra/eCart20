using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCart.Interfaces
{
    public interface iCartMgr
    {
        int getCartInfo(int id);

        void addItemToCart(int id, int qty);
        void addItemToCart(int id, int qty, string itemName, decimal price);
    }
}
