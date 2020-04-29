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
        List<CartDetail> getCartDetailsSummary(List<CartItem> cartItems);

        List<StorePickupPoint> GetStorePickupPoints(int storeId);
        StorePickupPoint GetStorePickup(int id);

        void addItemToCart(int id, int qty);
        void addItemToCart(int id, int qty, string itemName, decimal price);
        void addCartItemToDb(CartItem cartItem);
        void addCartDetailToDb(CartDetail cartDetail);

        void removeCartItem(int id);

        string saveOrder(List<CartDetail> cartDetails, List<CartItem> cartItems);

    }
}
