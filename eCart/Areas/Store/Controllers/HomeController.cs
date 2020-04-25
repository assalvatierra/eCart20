using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Store.Controllers
{
    public class HomeController : Controller
    {
        // GET: Store/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}