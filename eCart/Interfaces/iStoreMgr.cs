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

        void addNewStoreItem(int storeId, string itemName, decimal price);
        Models.StoreItem getStoreItem(int id);
        void updateStoreItem(int storeItemId, string itemName, decimal price);
        //add getdefault store pickuppoint
        //Models.StorePickupPoint getDefaultPickupPoint(int StoreId);
    }
}
