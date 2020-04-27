using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Store.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Shopper/Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() {
            return View();
        }

        // Store/Accounts/Login
        [HttpPost]
        public ActionResult Login( string username, string password )
        {
            Session["STOREID"] = "1";   //For test only
            return RedirectToAction("Index", "Home", new { area = "Store" });
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}