using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCart.Areas.Store.Models;
using eCart.Models;
using eCart.Services;

namespace eCart.Areas.Store.Controllers
{
    public class StoreDetailsController : Controller
    {
        private StoreContext db = new StoreContext();
        private ecartdbContainer edb = new ecartdbContainer();

        private StoreFactory storeFactory = new StoreFactory();

        // GET: Store/StoreDetails
        public ActionResult Index(int id)
        {

            var storeDetails = db.StoreDetails.Find(id);

            ViewBag.Status = db.StoreStatus.ToList();
            ViewBag.Cities = db.MasterCities.ToList();
            ViewBag.Areas = db.MasterAreas.ToList();

            return View(storeDetails);
        }

        // GET: Store/StoreDetails/Details/5
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

        // GET: Store/StoreDetails/Create
        public ActionResult Create()
        {
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name");
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name");
            return View();
        }

        // POST: Store/StoreDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LoginId,Name,Address,Remarks,StoreStatusId,StoreCategoryId,MasterCityId,MasterAreaId")] StoreDetail storeDetail)
        {
            if (ModelState.IsValid)
            {
                //db.StoreDetails.Add(storeDetail);
                //db.SaveChanges();

                storeFactory.StoreMgr.CreateStore(storeDetail);

                return RedirectToAction("Index");
            }

            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name", storeDetail.StoreStatusId);
            return View(storeDetail);
        }

        // GET: Store/StoreDetails/Edit/5
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
            ViewBag.StoreId = id;
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name", storeDetail.StoreStatusId);
            ViewBag.StoreImg = storeDetail.StoreImages.LastOrDefault().ImageUrl;
            return View(storeDetail);
        }

        // POST: Store/StoreDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LoginId,Name,Address,Remarks,StoreStatusId,StoreCategoryId,MasterCityId,MasterAreaId")] StoreDetail storeDetail, string ImgUrl)
        {
            if (ModelState.IsValid)
            {
                if (storeDetail.Remarks == null)
                {
                    storeDetail.Remarks = " ";

                }

                if (ImgUrl != null)
                {
                    //update store Img
                    //storeDetail.StoreImages.FirstOrDefault().ImageUrl = ImgUrl;
                    var storeImg = db.StoreImages.Where(s => s.StoreDetailId == storeDetail.Id).FirstOrDefault();
                    storeImg.ImageUrl = ImgUrl;
                    db.Entry(storeImg).State = EntityState.Modified;

                }

                //db.Entry(storeDetail).State = EntityState.Modified;
                //db.SaveChanges();

                storeFactory.StoreMgr.EditStore(storeDetail);

                return RedirectToAction("Index", "Home",new { area="Store", id = storeDetail.Id });
            }
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name", storeDetail.StoreStatusId);
            return View(storeDetail);
        }


        public void AddStoreImg(int id, string ImgUrl )
        {
            var StoreImage = new StoreImage()
            {
                ImageUrl = ImgUrl,
                StoreImgTypeId = 1,
                StoreDetailId = id
            };


            db.StoreImages.Add(StoreImage);
            db.SaveChanges();
        }

        // GET: Store/StoreDetails/Delete/5
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

        // POST: Store/StoreDetails/Delete/5
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
    }
}
