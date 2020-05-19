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
using eCart.Services;

namespace eCart.Areas.Store.Controllers
{
    public class StoreItemsController : Controller
    {
        private StoreContext db = new StoreContext();
        private StoreFactory storeFactory = new StoreFactory();

        // GET: Store/StoreItems
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                var storeItems = db.StoreItems.Where(s=>s.StoreDetailId == id).Include(s => s.ItemMaster).Include(s => s.StoreDetail).OrderByDescending(s=>s.Id);

                ViewBag.StoreId = id;

                return View(storeItems.ToList());
            }

            return RedirectToAction("Index", "Home");
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
        public ActionResult Create(int id)
        {
            StoreItem storeItem = new StoreItem() {
                StoreDetailId = id,
                UnitPrice = 0
            };

            ViewBag.ItemMasterId = new SelectList(db.ItemMasters, "Id", "Name");
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", id);
            return View(storeItem);
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
                //db.StoreItems.Add(storeItem);
                //db.SaveChanges();

                if (storeFactory.StoreMgr.AddStoreItem(storeItem))
                {
                    return RedirectToAction("Index", new { id = storeItem.StoreDetailId });
                }

                ModelState.AddModelError("", "Unable to add new item.");
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
                db.Entry(storeItem).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = storeItem.StoreDetailId });
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
            return RedirectToAction("Index", new { id = storeItem.StoreDetailId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //public PartialViewResult _ModalCreateItem()
        //{
        //    StoreItem item = new StoreItem();
        //    ViewBag.ItemMasterId = new SelectList(db.ItemMasters, "Id", "Name");
        //    ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
           
        //    return PartialView(item);
        //}


        [HttpPost]
        public bool AddStoreItem(int storeId, string itemName, decimal price, string imgUrl)
        {
            try
            {
                var storeMgr = storeFactory.StoreMgr;
                storeMgr.AddNewStoreItem(storeId, itemName, price, imgUrl);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public JsonResult GetStoreItem(int id)
        {
            var storeMgr = storeFactory.StoreMgr;
            var item = storeMgr.getStoreItem(id);

            var jsonItem = new jsonStoreItem
            {
                Id = id,
                ItemName = item.ItemMaster.Name,
                UnitPrice = item.UnitPrice,
                ImageUrl = item.ItemMaster.ItemImages.FirstOrDefault().ImageUrl
            };

            return Json(jsonItem, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItemMasters(int id)
        {
            var storeMgr = storeFactory.StoreMgr;
            var itemList = db.ItemMasterCategories.Where(s=>s.ItemCategoryId == id).ToList().Select(s => new { s.ItemMasterId, s.ItemMaster.Name });

            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateStoreItem(int storeItemId, string itemName, decimal price, string imageUrl)
        {
            try
            {
                var storeMgr = storeFactory.StoreMgr;
                storeMgr.updateStoreItem(storeItemId, itemName, price);
                storeMgr.updateStoreItemImage(storeItemId,imageUrl);
            }
            catch (Exception)
            {
            }
        }

        [HttpGet]
        public JsonResult GetItemCategories()
        {
            var categories = db.ItemCategories.Select(c => new { c.Id, c.Name }).ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool AddItemCategory(int itemId, int categoryId)
        {
            try
            {
                var item = db.StoreItems.Find(itemId);
                var masterItem = item.ItemMaster;
                var category = db.ItemCategories.Find(categoryId);

                var mastercategory = new ItemMasterCategory
                {
                    ItemCategory = category,
                    ItemMaster = masterItem
                };

                db.ItemMasterCategories.Add(mastercategory);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }

        }


        [HttpPost]
        public void RemoveItemCategory(int Id)
        {
            try
            {
                var itemMasterCategory = db.ItemMasterCategories.Find(Id);
                db.ItemMasterCategories.Remove(itemMasterCategory);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

public class jsonStoreItem {
    public int Id { get; set; }
    public string ItemName { get; set; }
    public decimal UnitPrice { get; set; }
    public string ImageUrl { get; set; }
}
