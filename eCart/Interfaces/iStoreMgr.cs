using eCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCart.Interfaces
{
    public interface iStoreMgr
    {
        List<Models.StoreDetail> getFeaturedStores();
        List<Models.StoreItem> getFeaturedItems();
        List<Models.StoreItem> getStoreItems(int id);
        Models.StoreDetail  getStoreDetails(int id);
        Models.StoreItem getStoreItem(int id);
        Models.StorePickupPoint getDefaultPickupPoint(int StoreId);
        StoreDetail getRandomStore();

        void addNewStoreItem(int storeId, string itemName, decimal price, string imgUrl);
        string addPaymentDetails(string date, int partyId, string partyInfo, int receiverId, string receiverInfo, int statusId, decimal amount, int cartDetailId);
        void addCartDeliveryActivity(int cartId, int statusId);

        void updateStoreItem(int storeItemId, string itemName, decimal price );
        void updateStoreItemImage(int storeItemId, string imageUrl);
    }
}
