using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCart.Services
{
    public class StoreMgr : Interfaces.iStoreMgr    
    {
       public List<Models.StoreDetail> getFeaturedStores()
        {
            try
            {
                return new List<Models.StoreDetail>();
            }
            catch
            { 
                throw new NotImplementedException();
            }
        }

        public List<Models.ItemMaster> getFeaturedItems()
        {
            try
            {
                return new List<Models.ItemMaster>();
            }
            catch
            {
                throw new NotImplementedException();
            }

        }
    }
}