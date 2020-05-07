using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCart.Areas.Rider.Model;
using eCart.Models;
using eCart.Services;

namespace eCart.Areas.Rider.Controllers
{
    public class RiderDetailsController : Controller
    {
        private RiderContext db = new RiderContext();
        private RiderMgr riderMgr = new RiderMgr();

        // GET: Rider/RiderDetails
        public ActionResult Index(int id)
        {
            var rider = db.RiderDetails.Find(id);
            ViewBag.Rider = rider;

            var riderCarts = db.CartDeliveries;
            return View(riderCarts.ToList());
        }

        // GET: Rider/RiderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderDetail riderDetail = db.RiderDetails.Find(id);
            if (riderDetail == null)
            {
                return HttpNotFound();
            }
            return View(riderDetail);
        }

        // GET: Rider/RiderDetails/Create
        public ActionResult Create()
        {
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.RiderStatusId = new SelectList(db.RiderStatus, "Id", "Name");
            return View();
        }

        // POST: Rider/RiderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,Address,Mobile,Remarks,RiderStatusId,MasterCityId,Mobile2")] RiderDetail riderDetail)
        {
            if (ModelState.IsValid)
            {
                db.RiderDetails.Add(riderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(db.RiderStatus, "Id", "Name", riderDetail.RiderStatusId);
            return View(riderDetail);
        }

        // GET: Rider/RiderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderDetail riderDetail = db.RiderDetails.Find(id);
            if (riderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(db.RiderStatus, "Id", "Name", riderDetail.RiderStatusId);
            return View(riderDetail);
        }

        // POST: Rider/RiderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Address,Mobile,Remarks,RiderStatusId,MasterCityId,Mobile2")] RiderDetail riderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(db.RiderStatus, "Id", "Name", riderDetail.RiderStatusId);
            return View(riderDetail);
        }

        // GET: Rider/RiderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderDetail riderDetail = db.RiderDetails.Find(id);
            if (riderDetail == null)
            {
                return HttpNotFound();
            }
            return View(riderDetail);
        }

        // POST: Rider/RiderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiderDetail riderDetail = db.RiderDetails.Find(id);
            db.RiderDetails.Remove(riderDetail);
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


        public ActionResult DeliveryDetails(int id)
        {
            CartDelivery delivery = db.CartDeliveries.Find(id);
            var paymentId = 0;
            if (db.RiderCashDetails.Where(s => s.CartDetailId == delivery.CartDetailId).Count() != 0)
            {
                paymentId = db.RiderCashDetails.Where(s => s.CartDetailId == delivery.CartDetailId).OrderByDescending(s => s.Id).FirstOrDefault().Id;
                ViewBag.RiderCashDetails = db.RiderCashDetails.Find(paymentId);
            }
            else
            {
                ViewBag.RiderCashDetails = null ;
            }

            ViewBag.DeliveryStatus = riderMgr.getLastestActivity(delivery.CartDetailId);
            ViewBag.RiderCashParty = db.RiderCashParties;

            return View(delivery);
        }

        [HttpPost]
        public void AddPayment(int riderDetailId, int cartDetailId, int riderCashPartyId,  decimal amount)
        {
            var riderCashDetail = new RiderCashDetail
            {
                Amount = amount,
                CartDetailId = cartDetailId,
                DtCash = DateTime.Now,
                RiderCashPartyId = riderCashPartyId,
                RiderDetailId = riderDetailId
            };

            riderMgr.AddCartPayment(riderCashDetail);

        }

        public void UpdateStatus(int id, int statusId)
        {
            riderMgr.AddDeliveryActivity(id, statusId);

            //on item delivered
            if(statusId == 4)
            {
                riderMgr.AddCartHistory(id, 5);
            }
        }
    }
}
