using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Models;

namespace eCart.Controllers
{
    public class HomeController : Controller
    {
        ecartdbContainer db = new ecartdbContainer();

        public ActionResult Index()
        {
            Services.StoreFactory store = new Services.StoreFactory();

           List<Models.StoreDetail> featuredstores = store.StoreMgr.getFeaturedStores();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AboutHow()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        // PartialView for Store List View
        public PartialViewResult _StoreList()
        {
            //take latest added stores
            var stores = db.StoreDetails.OrderByDescending(s => s.Id).Take(4);

            return PartialView(stores.ToList());
        }

        // PartialView for Store List View
        public PartialViewResult _ProductList()
        {
            //take latest added items
            var stores = db.StoreItems.OrderByDescending(s => s.Id).Take(10);

            return PartialView(stores.ToList());
        }

    }
}