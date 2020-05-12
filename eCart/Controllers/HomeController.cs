﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Models;
using eCart.Services;
using eCart.Interfaces;
using System.Collections;

namespace eCart.Controllers
{
    public class HomeController : Controller
    {
        StoreMgr storeMgr = new StoreMgr();

        public ActionResult Index()
        {
            Services.StoreFactory store = new Services.StoreFactory();

            List<Models.StoreDetail> featuredstores = store.StoreMgr.getFeaturedStores();

            // Session["USER"] = "Admin";   //For test only
            //  CreateCart();
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
            var featuredStores = storeMgr.getFeaturedStores().Take(3);

            return PartialView(featuredStores);
        }

        // PartialView for Store List View
        public PartialViewResult _ProductList()
        {
            var featuredItems = storeMgr.getFeaturedItems().Take(12);

            return PartialView(featuredItems);
        }


        //test only
        public string CreateCart()
        {
            if (Session["CARTDETAILS"] == null)
            {
                //List<cCartDetails> cartDetails = new List<cCartDetails>();
                //Session["CARTDETAILS"] = (List<cCartDetails>)cartDetails;
            }


            return "Cart Created";
        }



    }
}