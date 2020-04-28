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
                    //CartDetail = CreateCartDetail(storeItem.StoreDetailId),
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

                foreach (var item in group)
                {
                    item.CartDetail = tempCartDetail;   //assign CartDetail to the cartItem
                }
            }

            return cartDetail;
        }


        //create a new cart per store
        public CartDetail CreateCartDetail(int storeId)
        {
            CartDetail cartDetails = new CartDetail
            {
                UserDetailId = 1,               //TODO: change to USERID
                StoreDetailId = storeId,        
                CartStatusId = 1,               //default: active
                StorePickupPointId = 1  
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

        public void saveOrder(List<CartDetail> cartDetails, List<CartItem> cartItems)
        {
            try
            {
                foreach(var cart in cartDetails)
                {
                    //TODO : Add CartDetails save to DB
                }
            }
            catch (Exception)
            {
                throw new NotSupportedException();
            }
        }
    }
}