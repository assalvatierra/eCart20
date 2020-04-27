using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Services;

namespace eCart.Controllers
{
    public class MerchantController : Controller
    {
        StoreMgr storeMgr = new StoreMgr();

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products(int? id)
        {
            if(id != null)
            {
                int storeId = (int)id;
                var storeItems= storeMgr.getStoreItems(storeId);
                var storeDetails = storeMgr.getStoreDetails(storeId);

                ViewBag.StoreName = storeDetails.Name;
                ViewBag.StoreAddress = storeDetails.Address;

                return View(storeItems);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

    }
}