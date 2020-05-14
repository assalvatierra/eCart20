using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using eCart.Areas.Shopper.Models;
using eCart.Models;
using Microsoft.Ajax.Utilities;

namespace eCart.Areas.Shopper.Controllers
{
    public class UserDetailsController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Shopper/UserDetails
        public ActionResult Index(int id)
        {
            var user = db.Users.Find(id);
            var userDetails = db.UserDetails.Find(user.Id);
            return View(userDetails);
        }

        // GET: Shopper/UserDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // GET: Shopper/UserDetails/Create
        public ActionResult Create()
        {
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Name");
            return View();
        }

        // POST: Shopper/UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,Address,Email,Mobile,Remarks,UserStatusId,MasterCityId,MasterAreaId")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                db.UserDetails.Add(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Name", userDetail.UserStatusId);
            return View(userDetail);
        }

        // GET: Shopper/UserDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Name", userDetail.UserStatusId);
            return View(userDetail);
        }

        // POST: Shopper/UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Address,Email,Mobile,Remarks,UserStatusId,MasterCityId,MasterAreaId")] UserDetail userDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(userDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index" , new { id = userDetail.Id });
                }
            }
            catch (Exception)
            { }

            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Name", userDetail.UserStatusId);

            if (userDetail.Name.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Name", "Name field is empty.");
            }
            if (userDetail.Address.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Address", "Address field is empty.");
            }
            if (userDetail.Email.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Email", "Email field is empty.");
            }
            if (userDetail.Mobile.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Mobile", "Mobile field is empty.");
            }

            return View(userDetail);
        }

        // GET: Shopper/UserDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: Shopper/UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetail);
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
