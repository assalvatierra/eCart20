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
        int getUserId();
        int getCartInfo(int id);
        int getDefaultPickupPointId(int storeId);
        StorePickupPoint GetStorePickup(int id);
        List<cCart> getCartItems();
        List<CartItem> getCartSummary();
        List<CartDetail> getCartDetailsSummary();
        List<StorePickupPoint> GetStorePickupPoints(int storeId);
        List<CartDetail> getShopperCarts(int userId);
        List<PaymentReceiver> getPaymentRecievers();

        void addItemToCart(int id, int qty);
        void addItemToCart(int id, int qty, string itemName, decimal price);
        void addCartItemToDb(CartItem cartItem);
        void addCartDetailToDb(CartDetail cartDetail);
        void addCartHistory(CartDetail cart, CartStatus status, string userId);

        void updateCartPickupPoint(int cartId, int pickupPoint);
        void updateCartAsDelivery(int cartId);
        void updateCartDetailsStatus(int cartId, string status);
        string setCartPaymentReceiver(int cartId, int recieverId);
        void setCartPickupDate(int cartId, DateTime pickupdate);
        string setDBCartStatus(int cartId, int cartStatusId, string userId);
        string setCartStatusCancelled(int cartId, string userId);

        void removeCartItem(int id);
        void removeDBCartItem(int id, int statusId);

        string saveOrder(List<CartDetail> cartDetails);
        string saveOrder(CartDetail cart);

    }
}
