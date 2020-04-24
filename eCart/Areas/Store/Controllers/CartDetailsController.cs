using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCart.Areas.Store.Models;
using eCart.Models;

namespace eCart.Areas.Store.Controllers
{
    public class CartDetailsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Store/CartDetails
        public ActionResult Index()
        {
            var cartDetails = db.CartDetails.Include(c => c.CartStatu).Include(c => c.StoreDetail).Include(c => c.StorePickupPoint).Include(c => c.UserDetail);
            return View(cartDetails.ToList());
        }

        // GET: Store/CartDetails/Details/5
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

        // GET: Store/CartDetails/Create
        public ActionResult Create()
        {
            ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name");
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address");
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId");
            return View();
        }

        // POST: Store/CartDetails/Create
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

        // GET: Store/CartDetails/Edit/5
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

        // POST: Store/CartDetails/Edit/5
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

        // GET: Store/CartDetails/Delete/5
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

        // POST: Store/CartDetails/Delete/5
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
    }
}
