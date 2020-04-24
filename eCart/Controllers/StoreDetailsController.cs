using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCart.Models;

namespace eCart.Controllers
{
    public class StoreDetailsController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: StoreDetails
        public ActionResult Index()
        {
            var storeDetails = db.StoreDetails.Include(s => s.StoreStatu).Include(s => s.StoreCategory).Include(s => s.MasterCity).Include(s => s.MasterArea);
            return View(storeDetails.ToList());
        }

        // GET: StoreDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreDetail storeDetail = db.StoreDetails.Find(id);
            if (storeDetail == null)
            {
                return HttpNotFound();
            }
            return View(storeDetail);
        }

        // GET: StoreDetails/Create
        public ActionResult Create()
        {
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name");
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name");
            return View();
        }

        // POST: StoreDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LoginId,Name,Address,Remarks,StoreStatusId,StoreCategoryId,MasterCityId,MasterAreaId")] StoreDetail storeDetail)
        {
            if (ModelState.IsValid)
            {
                db.StoreDetails.Add(storeDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name", storeDetail.StoreStatusId);
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", storeDetail.MasterCityId);
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", storeDetail.MasterAreaId);
            return View(storeDetail);
        }

        // GET: StoreDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreDetail storeDetail = db.StoreDetails.Find(id);
            if (storeDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name", storeDetail.StoreStatusId);
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", storeDetail.MasterCityId);
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", storeDetail.MasterAreaId);
            return View(storeDetail);
        }

        // POST: StoreDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LoginId,Name,Address,Remarks,StoreStatusId,StoreCategoryId,MasterCityId,MasterAreaId")] StoreDetail storeDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name", storeDetail.StoreStatusId);
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", storeDetail.MasterCityId);
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", storeDetail.MasterAreaId);
            return View(storeDetail);
        }

        // GET: StoreDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreDetail storeDetail = db.StoreDetails.Find(id);
            if (storeDetail == null)
            {
                return HttpNotFound();
            }
            return View(storeDetail);
        }

        // POST: StoreDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreDetail storeDetail = db.StoreDetails.Find(id);
            db.StoreDetails.Remove(storeDetail);
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

        public ActionResult StoreList()
        {
            return View();
        }


        public ActionResult ItemList(int? id)
        {
            //var storeItems = db.StoreItems.Where(s => s.StoreDetailId == id);

            //return View(storeItems.ToList());
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
