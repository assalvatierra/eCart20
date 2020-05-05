using eCart.Areas.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Services;
using eCart.Models;

namespace eCart.Areas.Shopper.Controllers
{
    public class AccountsController : Controller
    {
        ecartdbContainer edb = new ecartdbContainer();
        ShopperContext db = new ShopperContext();
        AccMgr accMgr = new AccMgr();

        // GET: Shopper/Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        // Shopper/Accounts/Login
        [HttpPost]
        public ActionResult Login([Bind(Include ="Username, Password")] AccountLogin account )
        {
            var result = accMgr.checkLoginCredentials(account.Username, account.Password);
            if (result > 0)
            {
                Session["USERID"] = result;  
                Session["USER"] = account.Username;  
                CreateCart();

                return RedirectToAction("Index", "Home", new { area = "Shopper" });
            }
            else
            {
                Session["USERID"] = 1;
                Session["USER"] = "Admin";   //For test only
                CreateCart();

                return RedirectToAction("Index", "Home", new { area = "Shopper" });
            }
        }

        public ActionResult Logout()
        {
            Session["USER"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            ViewBag.UserStatusId = new SelectList(db.UserStatuses, "Id", "Name");

            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");

            ViewBag.UserStatusId = new SelectList(db.MasterAreas, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "Name, Address, Email, Mobile, Password")] AccountRegistration registration)
        {
            if (registration != null )
            {
                registration.UserStatusId = 1;
                registration.MasterAreaId = 1;
                registration.MasterCityId = 1;

                //register account
                accMgr.registerAccount(registration);

                // proceed to login
                return RedirectToAction("Login");
            }

            ViewBag.UserStatusId = new SelectList(edb.UserStatus, "Id", "Name", 1);
            ViewBag.MasterCityId = new SelectList(edb.MasterCities, "Id", "Name", 1);
            ViewBag.UserStatusId = new SelectList(edb.MasterAreas, "Id", "Name", 1);
            return View();
        }
        public string CreateCart()
        {
            List<cCart> cartItems = new List<cCart>();
            Session["MYCART"] = (List<cCart>)cartItems;

            List<cCartDetails> cartDetails = new List<cCartDetails>();
            Session["CARTDETAILS"] = (List<cCartDetails>)cartDetails;

            return "Cart Created";
        }




        #region functions

        #endregion

    }
}