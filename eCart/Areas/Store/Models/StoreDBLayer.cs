using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Models;
using eCart.Interfaces;
using System.Data.Entity;

namespace eCart.Areas.Store.Models
{
    public class StoreDBLayer : iStoreDb
    {
        StoreContext sdb = new StoreContext();
        ecartdbContainer db = new ecartdbContainer();

        #region Store Registration
        public StoreDetail GetStoreDetails(int id)
        {
            var store = sdb.StoreDetails.Find(id);
            return store;
        }

        public bool CreateStoreDetail(StoreDetail storeDetail)
        {
            try
            {
                db.StoreDetails.Add(storeDetail);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        public bool EditStoreDetail(StoreDetail storeDetail)
        {
            try
            {
                sdb.Entry(storeDetail).State = EntityState.Modified;
                sdb.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public StoreDetail GetStoreByUserId(string id)
        {
                if (sdb.StoreDetails.Any(s => s.LoginId == id))
                {
                    return sdb.StoreDetails.Where(s => s.LoginId == id).FirstOrDefault();
                }
                return null;
        }
        #endregion


    }
}