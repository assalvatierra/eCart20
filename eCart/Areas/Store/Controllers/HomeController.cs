using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Areas.Store.Models;

namespace eCart.Areas.Store.Controllers
{
    public class HomeController : Controller
    {
        StoreDB sdb = new StoreDB();

        // GET: Store/Home
        public ActionResult Index()
        {
            string STOREID = Session["STOREID"] != null ? Session["STOREID"].ToString() : "1";
            ViewBag.StoreId = int.Parse(STOREID);

            return View(sdb.getStoreDetails(STOREID));
        }
    }
}