﻿using System;
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
        private StoreFactory storeFactory = new StoreFactory();

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

        public ActionResult CartCheckout()
        {
            if (cartMgr.getUserId() != 0 )
            {
                var cartDetails = cartMgr.getCartDetailsSummary();
                ViewBag.PaymentParties = cartMgr.getPaymentRecievers();
                var userDetailId = cartMgr.getUserId();
                ViewBag.UserDetails = db.UserDetails.Find(userDetailId);
        
                return PartialView(cartDetails);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }


        [HttpGet]
        public JsonResult getSession()
        {
            var cartItems = cartMgr.getCartItems();
            return Json(cartItems, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public bool AddToCart(int id, int qty, decimal itemPrice)
        {
            try
            {
                return cartMgr.addItemToCart(id, qty, itemPrice);
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public string RemoveCartItem(int id)
        {
            try
            {
                if (cartMgr.removeCartItem(id)) {
                    return "Removed";
                }

                return "Error";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public bool SubmitOrder(int id)
        {
            try
            {
                if(cartMgr.getCartDetailsSummary().Find(s => s.Id == id) == null)
                {
                    return false;
                }

                var cartDetail = cartMgr.getCartDetailsSummary().Find(s => s.Id == id);

                if (cartDetail == null)
                {
                    return false;
                }

                return cartMgr.saveOrder(cartDetail);  //save to db

            }
            catch
            {
                return false;
            }


        }

        public bool SubmitAllOrder()
        {
            try
            {
                var cartDetails = cartMgr.getCartDetailsSummary();
                return cartMgr.saveOrder(cartDetails); //save to db

            }
            catch 
            {
                return false;
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
        public bool UpdatePickupPoint(int cartId, int pickupPointId)
        {
            try
            {
               return cartMgr.updateCartPickupPoint(cartId, pickupPointId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCartAsDelivery(int cartId)
        {
            try
            {
                return cartMgr.updateCartAsDelivery(cartId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        public JsonResult GetPaymentRecievers()
        {
            var paymentOptions = cartMgr.getPaymentRecievers().Select(s => new { s.Id , s.Description });
            return Json(paymentOptions, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool SetPaymentReceiver(int cartId, int receiverId)
        {
            try
            {
                receiverId = receiverId != 0 ? receiverId : 1;
                return cartMgr.setCartPaymentReceiver(cartId, receiverId);
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public bool SetCartPickupDate(int cartId, DateTime date)
        {
               return  cartMgr.setCartPickupDate(cartId, date);
           
        }


        public JsonResult getCart()
        {
            var cartList  = (List<cCartDetails>)Session["CARTDETAILS"] ?? new List<cCartDetails>();
            return Json(cartList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCurrentCart()
        {
            var cartList = cartMgr.getCartDetails();

            return Json(cartList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PendingCarts()
        {
            var userId = cartMgr.getUserId(); 
            var myCarts = cartMgr.getShopperCarts(userId).OrderByDescending(s=>s.Id).ToList();

            var userDetailId = cartMgr.getUserId();
            ViewBag.UserDetails = db.UserDetails.Find(userDetailId);

            return View(myCarts);
        }

        [HttpGet]
        public string CancelCart(int cartId)
        {
            var userId = cartMgr.getUserId();
            var result = cartMgr.setCartStatusCancelled(cartId, userId.ToString());
            return result;
        }

    }
}
