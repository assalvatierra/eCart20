using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Models;
using eCart.Areas.Store.Models;
using Microsoft.Ajax.Utilities;

namespace eCart.Services
{
    public class StoreMgr : Interfaces.iStoreMgr    
    {

        public ecartdbContainer db = new ecartdbContainer();
        public StoreDBLayer storeDb = new StoreDBLayer();


        #region For Revision

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

        public void addNewStoreItem(int storeId, string itemName, decimal price, string imgUrl)
        {
            try
            {
                //add item to item master
                ItemMaster item = new ItemMaster()
                {
                    Name = itemName,
                };

                db.ItemMasters.Add(item);

                //add item image
                ItemImage itemImage = new ItemImage() {
                    ItemMaster = item,
                    ImageUrl = imgUrl
                };
                db.ItemImages.Add(itemImage);


                StoreItem storeItem = new StoreItem()
                {
                    ItemMaster = item,
                    StoreDetailId = storeId,
                    UnitPrice = price
                };
                db.StoreItems.Add(storeItem);

                db.SaveChanges();
            }
            catch (Exception ex) 
            { 
                throw ex; 
            }
            
        }

        public StoreItem getStoreItem(int id)
        {
            try
            {
                return db.StoreItems.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void updateStoreItem(int storeItemId, string itemName, decimal price)
        {
                
            try
            {
                var storeItem = db.StoreItems.Find(storeItemId);
                storeItem.UnitPrice = price;
                storeItem.ItemMaster.Name = itemName;

                db.Entry(storeItem).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateStoreItemImage(int storeItemId, string imageUrl) 
        {
            try
            {
                var storeItem = getStoreItem(storeItemId);
                var item = db.ItemMasters.Find(storeItem.Id);
                var itemImg = item.ItemImages.FirstOrDefault();
                itemImg.ImageUrl = imageUrl;

                db.Entry(itemImg).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StorePickupPoint getDefaultPickupPoint(int StoreId)
        {
            try
            {
                return db.StoreDetails.Find(StoreId).StorePickupPoints.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string addPaymentDetails(string date, int partyId, string partyInfo, int receiverId, string receiverInfo, int statusId, decimal amount, int cartDetailId)
        {
            try
            {

                PaymentDetail payment = new PaymentDetail()
                {
                    Amount = amount,
                    CartDetailId = cartDetailId,
                    dtPayment = DateTime.Now,
                    PaymentPartyId = partyId,
                    PartyInfo = partyInfo,
                    PaymentReceiverId = receiverId,
                    ReceiverInfo = receiverInfo,
                    PaymentStatusId = statusId
                };

                db.PaymentDetails.Add(payment);
                db.SaveChanges();

                return "Payment Added";
            }
            catch (Exception ex)
            {
                return ex.Message.ToLower();
            }   
        }

        public StoreDetail getRandomStore()
        {
            try
            {
                var random = new Random();
                var storeListCount = db.StoreDetails.ToList().Count();

                var selectedID = random.Next(1, storeListCount);
                var store = db.StoreDetails.Find(selectedID);

                return store;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addCartDeliveryActivity(int cartId, int statusId)
        {
            
        }

        public List<CartDetail> getStoreActiveCarts(int id)
        {
            var storeCarts = db.CartDetails.Where(s => s.StoreDetailId == id && s.CartStatusId <= 5); //active

            return storeCarts.ToList();
        }

        #endregion

        public bool CreateStore(StoreDetail storeDetail)
        {
            return storeDb.CreateStoreDetail(storeDetail);
        }

        public bool EditStore(StoreDetail storeDetail)
        {
            return storeDb.EditStoreDetail(storeDetail);
        }

        public StoreDetail GetStoreDetailByLoginId(string loginId)
        {
            if (!loginId.IsNullOrWhiteSpace())
            {
                return storeDb.GetStoreByUserId(loginId);
            }
            return null;
          
        }
    }
}