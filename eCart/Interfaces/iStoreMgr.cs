﻿using eCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCart.Interfaces
{
    public interface iStoreMgr
    {
        void setDbLayer(iStoreDb storedblayer);
        List<Models.StoreDetail> getFeaturedStores();
        List<Models.StoreItem> getFeaturedItems();
        List<Models.StoreItem> getStoreItems(int id);
        List<CartDetail> getStoreActiveCarts(int id);
        Models.StoreDetail  getStoreDetails(int id);
        Models.StoreItem getStoreItem(int id);
        Models.StorePickupPoint getDefaultPickupPoint(int StoreId);
        StoreDetail getRandomStore();

        string addPaymentDetails(string date, int partyId, string partyInfo, int receiverId, string receiverInfo, int statusId, decimal amount, int cartDetailId);
        void addCartDeliveryActivity(int cartId, int statusId);


        //Business Layer

        //Store Registration
        bool CreateStore(StoreDetail storeDetail);
        bool RegisterStore(StoreRegistration newStore);
        bool EditStore(StoreDetail storeDetail);
        bool EditStoreImg(int storeId, string imgUrl, int imgTypeId);
        StoreDetail GetStoreDetailByLoginId(string loginId);
        StoreImage GetStoreImg(int storeId);
        bool IsStoreImgExist(int storeId);
        bool CreatStoreImg(int storeId, string imgUrl, int ImgTypeId);
        bool ValidateStoreImg(int storeId, string imgUrl);

        //Store Items
        bool AddStoreItem(StoreItem storeItem);
        bool AddStoreItem(int storeID, int itemId, decimal price);
        bool AddNewStoreItem(int storeId, string itemName, decimal price, string imgUrl);
        ItemMaster GetItemMaster(int id);
        List<ItemCatGroup> GetItemCatGroups();
        List<ItemCategory> GetItemCategories(int itemCatGroupId);
        bool EditStoreItem(int storeItemId, string itemName, decimal price);
        bool EditStoreItemImage(int storeItemId, string imageUrl);
        bool RemoveStoreItem(int id);

    }
}
