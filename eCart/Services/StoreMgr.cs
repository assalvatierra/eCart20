using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Models;

namespace eCart.Services
{
    public class StoreMgr : Interfaces.iStoreMgr    
    {
       public ecartdbContainer db = new ecartdbContainer();

       public List<Models.StoreDetail> getFeaturedStores()
        {
            try
            {
                //initial: take latest added stores
                var stores = db.StoreDetails.OrderByDescending(s => s.Id).Take(10);
                
                return stores.ToList();
            }
            catch
            { 
                throw new NotImplementedException();
            }
        }

        public List<Models.StoreItem> getFeaturedItems()
        {
            try
            {
                //take latest added items
                var items = db.StoreItems.OrderByDescending(s => s.Id).Take(12);

                return items.ToList();
            }
            catch
            {
                throw new NotImplementedException();
            }

        }


        public List<Models.StoreItem> getStoreItems(int id)
        {
            try
            {
                //take latest all store items
                var items = db.StoreItems.Where(i => i.StoreDetailId == id);
                items = items.OrderByDescending(s => s.Id).Take(18);

                return items.ToList();
            }
            catch
            {
                throw new NotImplementedException();
            }

        }

        public StoreDetail getStoreDetails(int id)
        {
            try
            {
                //take latest all store items
                var storeDetail = db.StoreDetails.Find(id);

                return storeDetail;
            }
            catch
            {
                throw new NotImplementedException();
            }

        }


    }
}