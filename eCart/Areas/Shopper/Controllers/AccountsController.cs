using eCart.Areas.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Shopper.Controllers
{
    public class AccountsController : Controller
    {
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
        public ActionResult Login(string username, string password)
        {
           Session["USER"] = "Admin";   //For test only
           CreateCart();
           return RedirectToAction("Index", "Home", new { area = "Shopper" });

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

    }
}