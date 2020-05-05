using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCart.Areas.Shopper;
using eCart.Models;

namespace eCart.Areas.Shopper.Controllers
{
    public class PaymentDetailsController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Shopper/PaymentDetails
        public ActionResult Index()
        {
            var paymentDetails = db.PaymentDetails.Include(p => p.CartDetail).Include(p => p.PaymentParty).Include(p => p.PaymentReceiver).Include(p => p.PaymentStatu);
            return View(paymentDetails.ToList());
        }

        // GET: Shopper/PaymentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            return View(paymentDetail);
        }

        // GET: Shopper/PaymentDetails/Create
        public ActionResult Create()
        {
            ViewBag.CartDetailId = new SelectList(db.CartDetails, "Id", "DeliveryType");
            ViewBag.PaymentPartyId = new SelectList(db.PaymentParties, "Id", "Name");
            ViewBag.PaymentReceiverId = new SelectList(db.PaymentReceivers, "Id", "Description");
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "Id", "Name");
            return View();
        }

        // POST: Shopper/PaymentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CartDetailId,Amount,dtPayment,PaymentReceiverId,ReceiverInfo,PaymentPartyId,PartyInfo,PaymentStatusId")] PaymentDetail paymentDetail)
        {
            if (ModelState.IsValid)
            {
                db.PaymentDetails.Add(paymentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartDetailId = new SelectList(db.CartDetails, "Id", "DeliveryType", paymentDetail.CartDetailId);
            ViewBag.PaymentPartyId = new SelectList(db.PaymentParties, "Id", "Name", paymentDetail.PaymentPartyId);
            ViewBag.PaymentReceiverId = new SelectList(db.PaymentReceivers, "Id", "Description", paymentDetail.PaymentReceiverId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "Id", "Name", paymentDetail.PaymentStatusId);
            return View(paymentDetail);
        }

        // GET: Shopper/PaymentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartDetailId = new SelectList(db.CartDetails, "Id", "DeliveryType", paymentDetail.CartDetailId);
            ViewBag.PaymentPartyId = new SelectList(db.PaymentParties, "Id", "Name", paymentDetail.PaymentPartyId);
            ViewBag.PaymentReceiverId = new SelectList(db.PaymentReceivers, "Id", "Description", paymentDetail.PaymentReceiverId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "Id", "Name", paymentDetail.PaymentStatusId);
            return View(paymentDetail);
        }

        // POST: Shopper/PaymentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CartDetailId,Amount,dtPayment,PaymentReceiverId,ReceiverInfo,PaymentPartyId,PartyInfo,PaymentStatusId")] PaymentDetail paymentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartDetailId = new SelectList(db.CartDetails, "Id", "DeliveryType", paymentDetail.CartDetailId);
            ViewBag.PaymentPartyId = new SelectList(db.PaymentParties, "Id", "Name", paymentDetail.PaymentPartyId);
            ViewBag.PaymentReceiverId = new SelectList(db.PaymentReceivers, "Id", "Description", paymentDetail.PaymentReceiverId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "Id", "Name", paymentDetail.PaymentStatusId);
            return View(paymentDetail);
        }

        // GET: Shopper/PaymentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            return View(paymentDetail);
        }

        // POST: Shopper/PaymentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            db.PaymentDetails.Remove(paymentDetail);
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
