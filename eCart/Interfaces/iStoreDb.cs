using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCart.Models;

namespace eCart.Interfaces
{
    public interface iStoreDb
    {
        StoreDetail GetStoreDetails(int id);
        StoreDetail GetStoreByUserId(string id);
        bool SaveChanges();

        //Store Registration
        bool CreateStoreDetail(StoreDetail storeDetail);
        bool EditStoreDetail(StoreDetail storeDetail);
        bool IsStoreImgExist(int storeId);
        StoreImage GetStoreImg(int storeId, int imgTypeId);
        bool CreateStoreImg(StoreImage storeImage);

        //Store Items
        bool AddStoreItem(StoreItem storeItem);
        bool AddItemMaster(ItemMaster itemMaster);
        bool AddItemImage(ItemImage itemImage);
        bool EditStoreItem(StoreImage storeImage);

    }
}
