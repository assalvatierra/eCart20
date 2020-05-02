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
        int getDefaultPickupPointId(int storeId);
        StorePickupPoint GetStorePickup(int id);
        List<cCart> getCartItems();
        List<CartItem> getCartSummary();
        List<CartDetail> getCartDetailsSummary();
        List<StorePickupPoint> GetStorePickupPoints(int storeId);
        List<CartDetail> getShopperCarts(int userId);

        void addItemToCart(int id, int qty);
        void addItemToCart(int id, int qty, string itemName, decimal price);
        void addCartItemToDb(CartItem cartItem);
        void addCartDetailToDb(CartDetail cartDetail);

        void updateCartPickupPoint(int cartId, int pickupPoint);
        void updateCartAsDelivery(int cartId);
        void updateCartDetailsStatus(int cartId, string status);

        void removeCartItem(int id);

        string saveOrder(List<CartDetail> cartDetails);
        string saveOrder(CartDetail cart);

    }
}
