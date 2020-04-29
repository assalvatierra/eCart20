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

        //add getdefault store pickuppoint
        //Models.StorePickupPoint getDefaultPickupPoint(int StoreId);
    }
}
