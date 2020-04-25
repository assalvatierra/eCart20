using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Admin.Controllers
{
    public class AdminLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AccountsController : Controller
    {
        // GET: Admin/Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Username, Password ")] AdminLogin adminLogin)
        {
           return RedirectToAction("Index","Home", new { area = "Admin" });
        }

    }
}