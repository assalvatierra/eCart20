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
    public class StoreItemsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Store/StoreItems
        public ActionResult Index()
        {
            var storeItems = db.StoreItems.Include(s => s.ItemMaster).Include(s => s.StoreDetail);
            return View(storeItems.ToList());
        }

        // GET: Store/StoreItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItem storeItem = db.StoreItems.Find(id);
            if (storeItem == null)
            {
                return HttpNotFound();
            }
            return View(storeItem);
        }

        // GET: Store/StoreItems/Create
        public ActionResult Create()
        {
            ViewBag.ItemMasterId = new SelectList(db.ItemMasters, "Id", "Name");
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
            return View();
        }

        // POST: Store/StoreItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemMasterId,StoreDetailId,UnitPrice")] StoreItem storeItem)
        {
            if (ModelState.IsValid)
            {
                db.StoreItems.Add(storeItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemMasterId = new SelectList(db.ItemMasters, "Id", "Name", storeItem.ItemMasterId);
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storeItem.StoreDetailId);
            return View(storeItem);
        }

        // GET: Store/StoreItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItem storeItem = db.StoreItems.Find(id);
            if (storeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemMasterId = new SelectList(db.ItemMasters, "Id", "Name", storeItem.ItemMasterId);
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storeItem.StoreDetailId);
            return View(storeItem);
        }

        // POST: Store/StoreItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemMasterId,StoreDetailId,UnitPrice")] StoreItem storeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemMasterId = new SelectList(db.ItemMasters, "Id", "Name", storeItem.ItemMasterId);
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storeItem.StoreDetailId);
            return View(storeItem);
        }

        // GET: Store/StoreItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItem storeItem = db.StoreItems.Find(id);
            if (storeItem == null)
            {
                return HttpNotFound();
            }
            return View(storeItem);
        }

        // POST: Store/StoreItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreItem storeItem = db.StoreItems.Find(id);
            db.StoreItems.Remove(storeItem);
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
