using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Areas.Store.Models;

namespace eCart.Areas.Store.Controllers
{

    public class AccountsController : Controller
    {
        private StoreContext db = new StoreContext(); 

        // GET: Shopper/Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() {

            var storeTest = db.StoreDetails.FirstOrDefault();

            ViewBag.username = "";

            return View();
        }

        // Store/Accounts/Login
        [HttpPost]
        public ActionResult Login( string username, string password )
        {
            Session["STOREID"] = "1";   //For test only
            var LoginId = 1;

            return RedirectToAction("Index", "Home", new { area = "Store", id = LoginId });
        }

        public ActionResult Logout()
        {
            Session["STOREID"] = null;
            return RedirectToAction("Login", "Accounts", new { area = "Store" });
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}