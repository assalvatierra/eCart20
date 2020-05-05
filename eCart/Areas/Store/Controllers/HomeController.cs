using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Areas.Store.Models;
using eCart.Services;

namespace eCart.Areas.Store.Controllers
{
    public class HomeController : Controller
    {
        StoreDB sdb = new StoreDB();
        StoreFactory storeFactory = new StoreFactory();

        // GET: Store/Home
        public ActionResult Index(int? id)
        {
            if (id != null )
            {
                var storeMgr = storeFactory.StoreMgr;

                string STOREID = Session["STOREID"] != null ? Session["STOREID"].ToString() : id.ToString();
                ViewBag.StoreId = id;

                var store = storeMgr.getStoreDetails((int)id);
                return View(store);
            }
            else
            {
                return RedirectToAction("Login","Accounts", new { area = "Store" });
            }
            
        }
    }
}