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
                    CartDetail = CreateCartDetail(),
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

        public CartDetail CreateCartDetail()
        {
            CartDetail cartDetails = new CartDetail
            {
                UserDetailId = 1,
                StoreDetailId = 1,
                CartStatusId = 1,
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
    }
}