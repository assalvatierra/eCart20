using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Rider.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Rider/Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            return RedirectToAction("Index", "RiderDetails", new { area = "Rider", id = 2 });
        }

        public ActionResult Register()
        {
            return View();
        }



    }
}