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

        public int getDefaultPickupPointId(int storeId)
        {
            var pickupPoint = db.StorePickupPoints.Where(s => s.StoreDetailId == storeId).FirstOrDefault();

            return pickupPoint.Id;
        }

        public void addItemToCart(int id, int qty, string itemName, decimal price)
        {
            try
            {
                var item = db.StoreItems.Find(id);

                //create cartItem
                var newItem = new cCart
                {
                    Id = id,
                    Name = item.ItemMaster.Name,
                    Price = price,
                    Qty = qty,
                    StoreId = item.StoreDetailId,
                    ItemImage = item.ItemMaster.ItemImages.FirstOrDefault() != null ? item.ItemMaster.ItemImages.FirstOrDefault().ImageUrl : ""
                };

                //create cartDetails
                var newCart = new cCartDetails
                {
                    StoreId = newItem.StoreId,
                    CartStatus = 1,
                    DeliveryType = "Pickup",
                    DtPickup = DateTime.Now,
                    PickupPointId = getDefaultPickupPointId(newItem.StoreId), //TODO : get default
                    cartItems = new List<cCart> { newItem }
                };

                var cartList = getCartDetails();
                var isAssigned = false;

                foreach (var cart in cartList)
                {
                    if (cart.StoreId == newItem.StoreId)
                    {
                        cart.cartItems.Add(newItem);
                        isAssigned = true;
                    }
                }

                if (isAssigned == false)
                {
                    cartList.Add(newCart);
                }

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addCartDetailToDb(CartDetail cartDetail)
        {
            try
            {
                db.CartDetails.Add(cartDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int getCartInfo(int id)
        {
            throw new NotImplementedException();
        }

        public List<cCart> getCartItems()
        {
            var cartDetails = getCartDetails();
            var cartItems = new List<cCart>();

            foreach(var cart in cartDetails)
            {
                foreach (var item in cart.cartItems)
                {
                    cartItems.Add(item);
                }
            }


            return cartItems;
        }


        public List<cCartDetails> getCartDetails()
        {
            return (List<cCartDetails>)HttpContext.Current.Session["CARTDETAILS"] ?? new List<cCartDetails>();
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

        public List<CartDetail> getCartDetailsSummary()
        {
            List<CartDetail> cartDetail = new List<CartDetail>();

            //var groupedCartItems = cartItems.GroupBy(s => s.StoreItem.StoreDetail.Id);

            var cartList = getCartDetails();

            foreach (var cart in cartList)
            {
                //create CartDetails for current group
                var tempCartDetail = CreateCartDetail(cart); //KEY = StoredDetail.Id
                cartDetail.Add(tempCartDetail);

            }

            return cartDetail;
        }


        //create a new cart per store
        public CartDetail CreateCartDetail(cCartDetails cart)
        {
            var tempStore = db.StoreDetails.Find(cart.StoreId);
            var pickup = db.StorePickupPoints.Find(cart.PickupPointId);
            CartDetail cartDetails = new CartDetail
            {
                UserDetailId = 1,               //TODO: change to USERID
                StoreDetailId = cart.StoreId,
                StoreDetail = tempStore,
                CartStatusId = 1,               //default: active
                CartStatu = db.CartStatus.Find(1),
                StorePickupPoint = pickup,
                StorePickupPointId = cart.PickupPointId,
                DeliveryType = cart.DeliveryType,
                DtPickup = cart.DtPickup,
                CartItems = getCartItems(cart)
            };

            return cartDetails;
        }


        //transfer to db cartItem object
        public List<CartItem> getCartItems(cCartDetails cart)
        {
            var items = new List<CartItem>();
            var cItems = cart.cartItems;
            int order = 1;

            foreach (var item in cart.cartItems)
            {
                items.Add(new CartItem
                {
                    CartDetailId = cart.Id,
                    CartItemStatusId = cart.CartStatus,
                    ItemOrder = order.ToString(),
                    ItemQty = item.Qty,
                    StoreItemId = item.Id,
                    StoreItem = db.StoreItems.Find(item.Id),
                    Remarks1 = item.remarks1,
                    Remarks2 = item.remarks2
                });
                order++;
            }

            return items;
        }

        public void removeCartItem(int id)
        {
            try
            {
                var cartList = getCartDetails();
                cartList.ForEach( (cart) => {
                        var selected = cart.cartItems.Find(i => i.Id == id);
                        cart.cartItems.Remove(selected);
                    });

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

                    cart.DtPickup = DateTime.Now; //TODO : update dtpickup and location on user input
                    addCartDetailToDb(cart);

                    //foreach (var item in cartItems)
                    //{
                    //    if (item.StoreItem.StoreDetailId == cart.StoreDetailId)
                    //    {
                    //        //assign cart to each item
                    //        item.CartDetail = cart;

                    //        //add to db
                    //        addCartItemToDb(item);
                    //    }
                    //}

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