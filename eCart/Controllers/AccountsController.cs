using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Services;
using eCart.Interfaces;

namespace eCart.Controllers
{
    public class AccountsController : Controller
    {
      
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        public void TestFactory()
        {
            

        }
    }
}