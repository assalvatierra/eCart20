using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eCart.Models;
using eCart.Services;
using eCart.Interfaces;
using eCart.Areas.Shopper.Models;
using System.Collections;

namespace eCart.Controllers
{
    public class HomeController : Controller
    {
        ecartdbContainer edb = new ecartdbContainer();
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
            var featuredStores = storeMgr.getFeaturedStores();

            return PartialView(featuredStores);
        }

        // PartialView for Store List View
        public PartialViewResult _ProductList()
        {
            var featuredItems = storeMgr.getFeaturedItems();

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


        //Authentication

        private readonly ecartdbContainer _dbContext = new ecartdbContainer();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = _dbContext.Users
               .Any(u => u.Username.ToLower() == user.Username.ToLower() && u.Password == user.Password);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["User"] = user.Username;
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        /// <summary>
        /// Login function 
        /// </summary>
        /// <param name="usertype">Integer: 15-admin, 1-Store, 0 or 2-shopper, 3-rider </param>
        /// <returns></returns>
        public ActionResult Register(int usertype)
        {
            ViewBag.UserType = usertype;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User registerUser)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Users.Add(registerUser);
                _dbContext.SaveChanges();
                return RedirectToAction("Login");

            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}