using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCart.Areas.Shopper.Models;
using eCart.Models;

namespace eCart.Interfaces
{
    public interface iCartMgr
    {
        int getCartInfo(int id);
        List<cCart> getCartItems();
        List<CartItem> getCartSummary();

        void addItemToCart(int id, int qty);
        void addItemToCart(int id, int qty, string itemName, decimal price);

        void removeCartItem(int id);
    }
}
