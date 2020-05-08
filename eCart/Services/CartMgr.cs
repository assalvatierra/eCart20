using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using eCart.Areas.Shopper.Models;
using eCart.Interfaces;
using eCart.Models;

namespace eCart.Services
{
    public class CartMgr: iCartMgr
    {
       protected ecartdbContainer db = new ecartdbContainer();

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
                    ItemImage = item.ItemMaster.ItemImages.FirstOrDefault() != null ? item.ItemMaster.ItemImages.FirstOrDefault().ImageUrl : "",
                    CartItemStatusId = 1    
                };

                //create cartDetails
                var newCart = new cCartDetails
                {
                    StoreId = newItem.StoreId,
                    CartStatus = 1,
                    DeliveryType = "Pickup",
                    DtPickup = DateTime.Now.AddHours(4),
                    PickupPointId = getDefaultPickupPointId(newItem.StoreId),
                    cartItems = new List<cCart> { newItem }
                };

                var cartList = getCartDetails();
                var isAssigned = false;

                foreach (var cart in cartList)
                {
                    if (cart.StoreId == newItem.StoreId)
                    {
                        if(cart.CartStatus == 1)
                        {
                            //add new item to the current active cart
                            newCart.Id = cartList.LastOrDefault().Id;
                            cart.cartItems.Add(newItem);
                            isAssigned = true;
                        }
                        else
                        {
                            newCart.Id = cartList.LastOrDefault().Id;
                            cartList.Add(newCart);
                            isAssigned = true;
                        }
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

            var cartList = getCartDetails();

            foreach (var cart in cartList)
            {
                //create CartDetails for current group
                var tempCartDetail = CreateCartDetail(cart); //KEY = StoredDetail.Id
                cartDetail.Add(tempCartDetail);

            }

            return cartDetail;
        }


        public List<PaymentReceiver> getPaymentRecievers()
        {
            try
            {
                return db.PaymentReceivers.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //create a new cart per store
        public CartDetail CreateCartDetail(cCartDetails cart)
        {
            var tempStore = db.StoreDetails.Find(cart.StoreId);
            var pickup = db.StorePickupPoints.Find(cart.PickupPointId);
            CartDetail cartDetails = new CartDetail
            {
                UserDetailId = getUserId(),  
                StoreDetailId = cart.StoreId,
                StoreDetail = tempStore,
                CartStatusId = cart.CartStatus,               //default: active
                CartStatu = db.CartStatus.Find(cart.CartStatus),
                StorePickupPoint = pickup,
                StorePickupPointId = cart.PickupPointId,
                DeliveryType = cart.DeliveryType,
                DtPickup = cart.DtPickup,
                CartItems = getCartItems(cart)
            };

            return cartDetails;
        }

        public int getUserId()
        {
            return HttpContext.Current.Session["USERID"] != null ? (int)HttpContext.Current.Session["USERID"] : 0;
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

        public void updateCartPickupPoint(int cartId, int pickupPointId)
        {
            try
            {
                var cart = getCartDetails().Find(s => s.Id == cartId);
                cart.PickupPointId = pickupPointId;
                cart.DeliveryType = "Pickup";

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public void updateCartAsDelivery(int cartId)
        {
            try
            {
                var cart = getCartDetails().Find(s => s.Id == cartId);
                cart.PickupPointId = db.StoreDetails.Find(cart.StoreId).StorePickupPoints.FirstOrDefault().Id;
                cart.DeliveryType = "Delivery";


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void removeCartItem(int id)
        {
            try
            {
                var cartList = getCartDetails();
                cartList.ForEach( (cart) => {
                   
                    //find the item and remove from the cart list
                    var selected = cart.cartItems.Find(i => i.Id == id);
                    cart.cartItems.Remove(selected);

                    //check if cart is empty, delete cart
                    if(cart.cartItems.Count == 0)
                    {
                        cartList.Remove(cart);
                    }
                });


            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public string saveOrder(List<CartDetail> cartDetails)
        {
            try
            {
                var userId = getUserId(); 

                foreach (var cart in cartDetails)
                {
                    //check if cart is active, then change status to submitted 
                    //and save to the db
                    if (cart.CartStatusId == 1 )
                    {
                        var cartStatus = db.CartStatus.Find(2);  //Submitted
                        cart.CartStatu = cartStatus;
                        addCartDetailToDb(cart);

                        //add cart history
                        addCartHistory(cart, cartStatus, userId.ToString());

                        //updateCartDetailsStatus(cart.Id, "Submitted");
                        removeCartSession(cart.Id);
                    }
                }

                db.SaveChanges();

                return "Order Submitted";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string saveOrder(CartDetail cart)
        {
            try
            {
                var userId = getUserId(); 
                //check if cart is active, then change status to submitted 
                //and save to the db
                if (cart.CartStatusId == 1)
                {
                    var cartStatus = db.CartStatus.Find(2);  //Submitted
                    cart.CartStatu = db.CartStatus.Find(2);  //Submitted
                    addCartDetailToDb(cart);

                    //add cart history
                    addCartHistory(cart, cartStatus, userId.ToString());

                    //updateCartDetailsStatus(cart.Id, "Submitted");
                    removeCartSession(cart.Id);
                }
               
                db.SaveChanges();

                return "Order Submitted";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public void updateCartDetailsStatus(int cartId, string status)
        {
            var cart = getCartDetails().Find(s=> s.Id == cartId);
            int statusId = db.CartStatus.Where(s=>s.Name.ToLower() == status.ToLower()).FirstOrDefault() != null ?
                db.CartStatus.Where(s => s.Name.ToLower() == status.ToLower()).FirstOrDefault().Id : 1;
            cart.CartStatus = statusId;

        }


        public string setCartPaymentReceiver(int cartId, int recieverId)
        {
            try
            {

                var cartpayment = new cCartPayment()
                {
                    Id = 1,
                    PaymentRecieverId = recieverId,
                    Amount = 0,
                    dtPayment = DateTime.Now,
                    PaymentStatusId = 1,    //pending
                    PartyInfo = "",
                    PaymentPartyId = 1,   //shopper
                    ReceiverInfo = ""
                };

                var cartList = getCartDetails();

                cartList.ForEach((c) => {
                    if (c.Id == cartId)
                    {
                        c.PaymentMode = db.PaymentReceivers.Find(recieverId).Description;


                        if (c.cartPayments == null )
                        {
                            c.cartPayments = new List<cCartPayment>();

                            //create new cartPayment
                            c.cartPayments.Add(cartpayment);
                        }
                        else
                        {
                            //add to the current list
                            c.cartPayments.Add(cartpayment);
                        }
                    }
                });

                return "Payment mode changes";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public void setCartPickupDate(int cartId, DateTime pickupdate)
        {
            //get session cart
            var cart = getCartDetails().Find(c=>c.Id == cartId);
            cart.DtPickup = pickupdate;

        }


        public void removeCartSession(int id)
        {
            var cartlist = getCartDetails();
            cartlist.Remove(cartlist.Find(c=>c.Id == id));
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

        public List<CartDetail> getShopperCarts(int userId)
        {
            var cartList = db.CartDetails.Where(s => s.UserDetailId == userId).ToList();
            return cartList;
        }

        public List<CartHistory> getCartHistory(int id)
        {
            return db.CartHistories.Where(s => s.CartDetailId == id).ToList();
        }

        public List<CartActivity> getCartDeliveryActivities(int id)
        {
            return db.CartActivities.Where(c => c.CartDeliveryId == id).OrderByDescending(s=>s.Id).ToList();
        }


        public void addCartHistory(CartDetail cart , CartStatus status, string userId)
        {
            try
            {
                var cartHistory = new CartHistory
                {
                    CartDetail = cart,
                    CartStatu = status,
                    dtStatus = DateTime.Now,
                    UserId = userId,
                };

                db.CartHistories.Add(cartHistory);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string setDBCartStatus(int cartId, int cartStatusId, string userId)
        {
            try
            {
                CartDetail cart = db.CartDetails.Find(cartId);
                CartStatus cartStatus = db.CartStatus.Find(cartStatusId);

                cart.CartStatu = cartStatus;
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();

                addCartHistory(cart, cartStatus, getUserId().ToString());
                return "cart status updated";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string setCartStatusCancelled(int cartId, string userId)
        {

            var cartstatusId = db.CartDetails.Find(cartId).CartStatusId;
            if (cartstatusId < 3) 
            {
                // if cart is still on Pending or Active, 
                // user is allowed to cancel the cart
                setDBCartStatus(cartId, 6, userId);
                return "Order Cancelled";
            }
            else
            {
                return "Order is now being processed";
            }

        }

        public void removeDBCartItem(int id, int statusId)
        {

            try
            {
                CartItem cartItem = db.CartItems.Find(id);
                cartItem.CartItemStatusId = 2;

                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void addDeliveryDetails(int id, DateTime date, string address, int riderId, string remarks)
        {
            try
            {
                var deliveryDetails = new CartDelivery
                {
                    CartDetailId = id,
                    dtDelivery = date,
                    Address = address,
                    RiderDetailId = riderId,
                    Remarks = remarks
                };

                db.CartDeliveries.Add(deliveryDetails);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateCartDelivery(CartDelivery cartDelivery)
        {
            try
            {
                var deliveryDetails = db.CartDeliveries.Find(cartDelivery.Id);
                deliveryDetails.RiderDetailId = cartDelivery.RiderDetailId;
                deliveryDetails.dtDelivery = cartDelivery.dtDelivery;
                deliveryDetails.Address = cartDelivery.Address;
                deliveryDetails.Remarks = cartDelivery.Remarks;

                db.Entry(deliveryDetails).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void removeCartdelivery(int id)
        {
            try
            {
            var deliveryDetails = db.CartDeliveries.Find(id);

            db.CartDeliveries.Remove(deliveryDetails);
            db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}