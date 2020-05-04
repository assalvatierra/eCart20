using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCart.Areas.Shopper.Models;
using eCart.Models;
using eCart.Services;

namespace eCart.Areas.Shopper.Controllers
{
    public class CartDetailsController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();
        private CartMgr cartMgr = new CartMgr();

        // GET: Shopper/CartDetails
        public ActionResult Index()
        {
            var cartDetails = db.CartDetails.Include(c => c.CartStatu).Include(c => c.StoreDetail).Include(c => c.StorePickupPoint).Include(c => c.UserDetail);
            return View(cartDetails.ToList());
        }

        // GET: Shopper/CartDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // GET: Shopper/CartDetails/Create
        public ActionResult Create()
        {
            ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name");
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address");
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId");
            return View();
        }

        // POST: Shopper/CartDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserDetailId,StoreDetailId,CartStatusId,StorePickupPointId")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                db.CartDetails.Add(cartDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name", cartDetail.CartStatusId);
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", cartDetail.StoreDetailId);
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", cartDetail.StorePickupPointId);
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", cartDetail.UserDetailId);
            return View(cartDetail);
        }

        // GET: Shopper/CartDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name", cartDetail.CartStatusId);
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", cartDetail.StoreDetailId);
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", cartDetail.StorePickupPointId);
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", cartDetail.UserDetailId);
            return View(cartDetail);
        }

        // POST: Shopper/CartDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserDetailId,StoreDetailId,CartStatusId,StorePickupPointId")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name", cartDetail.CartStatusId);
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", cartDetail.StoreDetailId);
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", cartDetail.StorePickupPointId);
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", cartDetail.UserDetailId);
            return View(cartDetail);
        }

        // GET: Shopper/CartDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // POST: Shopper/CartDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartDetail cartDetail = db.CartDetails.Find(id);
            db.CartDetails.Remove(cartDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Cart()
        {
            var cartSummary = cartMgr.getCartSummary();
            return View(cartSummary);
        }

        public PartialViewResult _CartSummary()
        {
            var cartItems = cartMgr.getCartItems();
           
            return PartialView(cartItems);
        }

        public PartialViewResult CartCheckout()
        {
            var cartDetails = cartMgr.getCartDetailsSummary();
            ViewBag.PaymentParties = cartMgr.getPaymentRecievers();
        
            return PartialView(cartDetails);
        }


        [HttpGet]
        public JsonResult getSession()
        {
            var cartItems = cartMgr.getCartItems();
            return Json(cartItems, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public string AddToCart(int id, int qty, string itemName, decimal itemPrice)
        {
            try
            {
                cartMgr.addItemToCart(id, qty, itemName, itemPrice);

                return "1";
            }catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpPost]
        public string RemoveCartItem(int id)
        {
            try
            {
                cartMgr.removeCartItem(id);

                return "1";
            }catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public string SubmitOrder(int id)
        {
            try
            {
                var msg = "0";
               
                int cartDetailId = id;
                 var cartDetail = cartMgr.getCartDetailsSummary().Find(s => s.Id == id);
                msg = cartMgr.saveOrder(cartDetail);  //save to db

                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        public string SubmitAllOrder()
        {
            try
            {
                var cartDetails = cartMgr.getCartDetailsSummary();
                var msg = cartMgr.saveOrder(cartDetails); //save to db

                return msg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [HttpGet]
        public JsonResult GetStorePickups(int storeId)
        {
            var locations = cartMgr.GetStorePickupPoints(storeId).Select(s => new { s.Id, s.Address });

            return Json(locations, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetStorePickupAddress(int id)
        {
            var location = cartMgr.GetStorePickup(id).Address;

            return Json(location, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdatePickupPoint(int cartId, int pickupPointId)
        {
            cartMgr.updateCartPickupPoint(cartId, pickupPointId);

        }

        public void UpdateCartAsDelivery(int cartId)
        {
            cartMgr.updateCartAsDelivery(cartId);

        }

        [HttpGet]
        public JsonResult GetPaymentRecievers()
        {
            var paymentOptions = cartMgr.getPaymentRecievers().Select(s => new { s.Id , s.Description });
            return Json(paymentOptions, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string SetPaymentReceiver(int cartId, int receiverId)
        {
           receiverId = receiverId != 0 ? receiverId : 1;
           return cartMgr.setCartPaymentReceiver(cartId, receiverId);
        }

        [HttpPost]
        public void SetCartPickupDate(int cartId, DateTime date)
        {
          
           cartMgr.setCartPickupDate(cartId, date);
          
        }



        #region CartDetails
        /* Revision of Cart */
        public JsonResult AddCartItem(int id, int qty, string itemName, decimal itemPrice)
        {
            try
            {
                 var item = db.StoreItems.Find(id);

                //create cartItem
                var newItem = new cCart
                {
                    Id = id,
                    Name = item.ItemMaster.Name,
                    Price = item.UnitPrice,
                    Qty = qty,
                    StoreId = item.StoreDetailId
                };

                //create cartDetails
                var newCart = new cCartDetails
                {
                    Id = 0,
                    StoreId = newItem.StoreId,
                    CartStatus = 1,
                    DeliveryType = "Pickup",
                    DtPickup = new DateTime(),
                    PickupPointId = 1, //TODO : get default
                    cartItems = new List<cCart> { newItem }
                };

                var cartList = cartMgr.getCartDetails();
                var isAssigned = false;
                   
                    foreach (var cart in cartList)
                    {
                        if (cart.StoreId == newItem.StoreId)
                        {
                            cart.cartItems.Add(newItem);
                            isAssigned = true;
                        }
                    }
                
                if(isAssigned == false)
                {
                    cartList.Add(newCart);
                }

                return Json(cartList, JsonRequestBehavior.AllowGet); 
            }
            catch(Exception)
            {
                return Json("NA", JsonRequestBehavior.AllowGet);
                //return ex.ToString();
            }

        }

        public List<cCartDetails> getCart()
        {
            return (List<cCartDetails>)Session["CARTDETAILS"] ?? new List<cCartDetails>();
        }

        public JsonResult getCurrentCart()
        {
            var cartList = cartMgr.getCartDetails();

            return Json(cartList, JsonRequestBehavior.AllowGet);
        }

        /* */
        #endregion

        public ActionResult PendingCarts()
        {
            var userId = 1; //TODO: get current user;
            var myCarts = cartMgr.getShopperCarts(userId).OrderByDescending(s=>s.Id).ToList();

            return View(myCarts);
        }


    }
}
