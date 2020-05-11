using eCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eCart.Areas.Rider.Controllers
{
    public class AccountsController : Controller
    {
        //Authentication
        private readonly ecartdbContainer _dbContext = new ecartdbContainer();

        // GET: Rider/Account
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(string username, string password)
        //{
        //    return RedirectToAction("Index", "RiderDetails", new { area = "Rider", id = 1 });
        //}


        /// <summary>
        /// Login function 
        /// </summary>
        /// <param name="usertype">Integer: 15-admin, 1-Store, 0 or 2-shopper, 3-rider </param>
        /// <returns></returns>
        public ActionResult Login(int? usertype)
        {
            ViewBag.UserType = usertype; //use for altering the login page info/images and other info to fit the type of user
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = _dbContext.Users
               .Any(u => u.Username.ToLower() == user.Username.ToLower() && u.Password == user.Password);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["USER"] = user.Username;

                    return RedirectToAction("Index", "RiderDetails", new { area = "Rider", id = 1 });

                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session["USER"] = null;
            return RedirectToAction("Login", "Accounts", new { area = "Store" });
        }

    }
}