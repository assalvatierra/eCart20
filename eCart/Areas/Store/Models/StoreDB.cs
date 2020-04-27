using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Models;

namespace eCart.Areas.Store.Models
{
    public class StoreDB 
    {
        StoreContext sdb = new StoreContext();

        public StoreDetail getStoreDetails(int id)
        {
            var store = sdb.StoreDetails.Find(id);
            return store;
        }

        public StoreDetail getStoreDetails(string STOREID)
        {
            int id = int.Parse(STOREID);
            var store = sdb.StoreDetails.Find(id);

            return store;
        }

    }
}