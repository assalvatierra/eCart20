using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eCart.Services;
using eCart.Interfaces;
using eCart.Models;


namespace eCart.Controllers
{
    public class AccountsController : Controller
    {

        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        //Authentication
        private readonly ecartdbContainer _dbContext = new ecartdbContainer();

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
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = _dbContext.Users
               .Any(u => u.Username.ToLower() == user.Username.ToLower() && u.Password == user.Password);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["User"] = user.Username;
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        /// <summary>
        /// Login function 
        /// </summary>
        /// <param name="usertype">Integer: 15-admin, 1-Store, 0 or 2-shopper, 3-rider </param>
        /// <returns></returns>
        public ActionResult Register(int? usertype)
        {
            ViewBag.UserType = usertype;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User registerUser)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Users.Add(registerUser);
                _dbContext.SaveChanges();
                return RedirectToAction("Login");

            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}