﻿using System;
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

                ViewBag.StoreName = storeDetails.Name;
                ViewBag.StoreAddress = storeDetails.Address;
                ViewBag.StoreImg = storeDetails.StoreImages.FirstOrDefault().ImageUrl;
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