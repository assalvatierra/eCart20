using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Controllers
{
    public class MerchantController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Products()
        {
            return View();
        }
    }
}