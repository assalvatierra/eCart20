using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Models;
using eCart.Services;

namespace eCart.Controllers
{
    public class MerchantController : Controller
    {
        StoreMgr storeMgr = new StoreMgr();

        // GET: Store
        public ActionResult Index()
        {
            var storeList = storeMgr.getFeaturedStores();
            return View(storeList);
        }

        public ActionResult Products(int? id)
        {
            if(id != null)
            {
                int storeId = (int)id;
                var storeItems= storeMgr.getStoreItems(storeId);
                var storeDetails = storeMgr.getStoreDetails(storeId);
                var defaultImg = "/img/placeholders/placeholder-product.png";

                ViewBag.StoreName = storeDetails.Name;
                ViewBag.StoreAddress = storeDetails.Address;
                ViewBag.StoreImg = storeDetails.StoreImages.FirstOrDefault() != null ? storeDetails.StoreImages.FirstOrDefault().ImageUrl : defaultImg;

                //Get next suggested Store
                StoreDetail store = new StoreDetail();
                do
                {
                    store = storeMgr.getRandomStore();
                } while (store.Id == id);

                ViewBag.nextStoreId = store.Id;
                ViewBag.nextStore = store.Name;
                ViewBag.nextStoreImg = store.StoreImages.FirstOrDefault().ImageUrl;
                return View(storeItems);
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }


        public ActionResult ProductDetails(int? id)
        {
            if (id != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Error");
            }
        }

    }
}