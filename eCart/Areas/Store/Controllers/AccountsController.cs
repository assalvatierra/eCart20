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

        public ActionResult Register()
        {
            return View();
        }
    }
}