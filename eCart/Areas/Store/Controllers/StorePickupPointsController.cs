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
    public class StorePickupPointsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Store/StorePickupPoints
        public ActionResult Index()
        {
            var storePickupPoints = db.StorePickupPoints.Include(s => s.StoreDetail).Include(s => s.StorePickupStatu);
            return View(storePickupPoints.ToList());
        }

        // GET: Store/StorePickupPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePickupPoint storePickupPoint = db.StorePickupPoints.Find(id);
            if (storePickupPoint == null)
            {
                return HttpNotFound();
            }
            return View(storePickupPoint);
        }

        // GET: Store/StorePickupPoints/Create
        public ActionResult Create()
        {
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
            ViewBag.StorePickupStatusId = new SelectList(db.StorePickupStatus, "Id", "Name");
            return View();
        }

        // POST: Store/StorePickupPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoreDetailId,Address,Remarks,StorePickupStatusId")] StorePickupPoint storePickupPoint)
        {
            if (ModelState.IsValid)
            {
                db.StorePickupPoints.Add(storePickupPoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storePickupPoint.StoreDetailId);
            ViewBag.StorePickupStatusId = new SelectList(db.StorePickupStatus, "Id", "Name", storePickupPoint.StorePickupStatusId);
            return View(storePickupPoint);
        }

        // GET: Store/StorePickupPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePickupPoint storePickupPoint = db.StorePickupPoints.Find(id);
            if (storePickupPoint == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storePickupPoint.StoreDetailId);
            ViewBag.StorePickupStatusId = new SelectList(db.StorePickupStatus, "Id", "Name", storePickupPoint.StorePickupStatusId);
            return View(storePickupPoint);
        }

        // POST: Store/StorePickupPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoreDetailId,Address,Remarks,StorePickupStatusId")] StorePickupPoint storePickupPoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storePickupPoint).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storePickupPoint.StoreDetailId);
            ViewBag.StorePickupStatusId = new SelectList(db.StorePickupStatus, "Id", "Name", storePickupPoint.StorePickupStatusId);
            return View(storePickupPoint);
        }

        // GET: Store/StorePickupPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePickupPoint storePickupPoint = db.StorePickupPoints.Find(id);
            if (storePickupPoint == null)
            {
                return HttpNotFound();
            }
            return View(storePickupPoint);
        }

        // POST: Store/StorePickupPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StorePickupPoint storePickupPoint = db.StorePickupPoints.Find(id);
            db.StorePickupPoints.Remove(storePickupPoint);
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
