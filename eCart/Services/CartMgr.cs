using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Areas.Shopper.Models;
using eCart.Models;

namespace eCart.Services
{
    public class CartMgr: Interfaces.iCartMgr
    {
        ecartdbContainer db = new ecartdbContainer();

        public void addItemToCart(int id, int qty)
        {
            throw new NotImplementedException();
        }

        public void addItemToCart(int id, int qty, string itemName, decimal price)
        {
            try
            {
                List<cCart> cartItems = getCartItems();

                cCart cart = new cCart
                {
                    Id = id,
                    Qty = qty,
                    Name = itemName,
                    Price = price
                };

                cartItems.Add(cart);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        public void addCartItemToDb(CartItem cartItem)
        {
            try
            {
                db.CartItems.Add(cartItem);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public void addCartDetailToDb(CartDetail cartDetail)
        {
            try
            {
                db.CartDetails.Add(cartDetail);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public int getCartInfo(int id)
        {
            throw new NotImplementedException();
        }

        public List<cCart> getCartItems()
        {
            return (List<cCart>)HttpContext.Current.Session["MYCART"] ?? new List<cCart>();
        }

        public List<CartItem> getCartSummary()
        {
            var carts = getCartItems();

            //transfer cCart to cartItems 
            List<CartItem> cartItems = new List<CartItem>();

            var order = 1;
            foreach (var item in carts)
            {
                var storeItem = db.StoreItems.Find(item.Id);

                cartItems.Add(new CartItem
                {
                    CartDetailId = 1,
                    StoreItemId = item.Id,
                    StoreItem = storeItem,
                    ItemQty = item.Qty,
                    ItemOrder = order.ToString(),
                    CartItemStatusId = 1,
                    Remarks1 = "",
                    Remarks2 = ""
                });

                order++;
            }

            return cartItems;
        }

        public List<CartDetail> getCartDetailsSummary(List<CartItem> cartItems)
        {
            List<CartDetail> cartDetail = new List<CartDetail>();

            var groupedCartItems = cartItems.GroupBy(s => s.StoreItem.StoreDetail.Id);

            foreach (var group in groupedCartItems)
            {
                //create CartDetails for current group
                var tempCartDetail = CreateCartDetail(group.Key); //KEY = StoredDetail.Id
                cartDetail.Add(tempCartDetail);

            }

            return cartDetail;
        }


        //create a new cart per store
        public CartDetail CreateCartDetail(int storeId)
        {
            var tempStore = db.StoreDetails.Find(storeId);
            CartDetail cartDetails = new CartDetail
            {
                UserDetailId = 1,               //TODO: change to USERID
                StoreDetailId = storeId,
                StoreDetail = tempStore,
                CartStatusId = 1,               //default: active
                CartStatu = db.CartStatus.Find(1),
                StorePickupPoint = tempStore.StorePickupPoints.FirstOrDefault(),
                StorePickupPointId = tempStore.StorePickupPoints.FirstOrDefault().Id
            };

            return cartDetails;
        }

        public void removeCartItem(int id)
        {
            try
            {
                var cartItems = getCartItems();
                var selectedItem = cartItems.Find(s => s.Id == id);
                cartItems.Remove(selectedItem);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public string saveOrder(List<CartDetail> cartDetails, List<CartItem> cartItems)
        {
            try
            {
                foreach (var cart in cartDetails)
                {
                    addCartDetailToDb(cart);

                    foreach (var item in cartItems)
                    {
                        if (item.StoreItem.StoreDetailId == cart.StoreDetailId)
                        {
                            //assign cart to each item
                            item.CartDetail = cart;

                            //add to db
                            addCartItemToDb(item);
                        }
                    }

                }

                db.SaveChanges();

                return "Order Submitted";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<StorePickupPoint> GetStorePickupPoints(int storeId)
        {
            try
            {
                return db.StoreDetails.Find(storeId).StorePickupPoints.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public StorePickupPoint GetStorePickup(int id)
        {
            try
            {
                return db.StorePickupPoints.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}