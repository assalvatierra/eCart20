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
        List<CartHistory> getCartHistory(int id);
        List<CartActivity> getCartDeliveryActivities(int id);

        void addItemToCart(int id, int qty);
        bool addItemToCart(int id, int qty, decimal price);
        void addCartItemToDb(CartItem cartItem);
        bool addCartDetailToDb(CartDetail cartDetail);
        bool addCartHistory(CartDetail cart, CartStatus status, string userId);
        void addDeliveryDetails(int id, DateTime date, string address, int riderId, string remarks);

        void updateCartPickupPoint(int storeId, int pickupPoint);
        void updateCartAsDelivery(int cartId);
        void updateCartDetailsStatus(int cartId, string status);
        void updateCartDelivery(CartDelivery cartDelivery);
        void setCartPickupDate(int cartId, DateTime pickupdate);
        string setCartPaymentReceiver(int cartId, int recieverId);
        string setDBCartStatus(int cartId, int cartStatusId, string userId);
        string setCartStatusCancelled(int cartId, string userId);

        bool removeCartItem(int id);
        void removeDBCartItem(int id, int statusId);
        void removeCartdelivery(int id);
        bool removeCartSession(int storeId);

        string saveOrder(List<CartDetail> cartDetails);
        string saveOrder(CartDetail cart);

    }
}
