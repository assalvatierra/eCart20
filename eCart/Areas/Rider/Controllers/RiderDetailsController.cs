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

namespace eCart.Areas.Rider.Controllers
{
    public class RiderDetailsController : Controller
    {
        private RiderContext db = new RiderContext();

        // GET: Rider/RiderDetails
        public ActionResult Index()
        {
            var riderDetails = db.RiderDetails.Include(r => r.MasterCity).Include(r => r.RiderStatu);
            return View(riderDetails.ToList());
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
    }
}
