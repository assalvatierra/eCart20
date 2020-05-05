﻿using System;
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
    public class StorePaymentsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Store/StorePayments
        public ActionResult Index()
        {
            var storePayments = db.StorePayments.Include(s => s.StoreDetail).Include(s => s.StorePaymentStatu).Include(s => s.StorePaymentType);
            return View(storePayments.ToList());
        }

        // GET: Store/StorePayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePayment storePayment = db.StorePayments.Find(id);
            if (storePayment == null)
            {
                return HttpNotFound();
            }
            return View(storePayment);
        }

        // GET: Store/StorePayments/Create
        public ActionResult Create()
        {
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
            ViewBag.StorePaymentStatusId = new SelectList(db.StorePaymentStatus, "Id", "Name");
            ViewBag.StorePaymentTypeId = new SelectList(db.StorePaymentTypes, "Id", "Description");
            return View();
        }

        // POST: Store/StorePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoreDetailId,dtPayment,Amount,StorePaymentTypeId,Remarks,dtPosted,StorePaymentStatusId")] StorePayment storePayment)
        {
            if (ModelState.IsValid)
            {
                db.StorePayments.Add(storePayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storePayment.StoreDetailId);
            ViewBag.StorePaymentStatusId = new SelectList(db.StorePaymentStatus, "Id", "Name", storePayment.StorePaymentStatusId);
            ViewBag.StorePaymentTypeId = new SelectList(db.StorePaymentTypes, "Id", "Description", storePayment.StorePaymentTypeId);
            return View(storePayment);
        }

        // GET: Store/StorePayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePayment storePayment = db.StorePayments.Find(id);
            if (storePayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storePayment.StoreDetailId);
            ViewBag.StorePaymentStatusId = new SelectList(db.StorePaymentStatus, "Id", "Name", storePayment.StorePaymentStatusId);
            ViewBag.StorePaymentTypeId = new SelectList(db.StorePaymentTypes, "Id", "Description", storePayment.StorePaymentTypeId);
            return View(storePayment);
        }

        // POST: Store/StorePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoreDetailId,dtPayment,Amount,StorePaymentTypeId,Remarks,dtPosted,StorePaymentStatusId")] StorePayment storePayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storePayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storePayment.StoreDetailId);
            ViewBag.StorePaymentStatusId = new SelectList(db.StorePaymentStatus, "Id", "Name", storePayment.StorePaymentStatusId);
            ViewBag.StorePaymentTypeId = new SelectList(db.StorePaymentTypes, "Id", "Description", storePayment.StorePaymentTypeId);
            return View(storePayment);
        }

        // GET: Store/StorePayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePayment storePayment = db.StorePayments.Find(id);
            if (storePayment == null)
            {
                return HttpNotFound();
            }
            return View(storePayment);
        }

        // POST: Store/StorePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StorePayment storePayment = db.StorePayments.Find(id);
            db.StorePayments.Remove(storePayment);
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
