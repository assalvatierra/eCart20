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