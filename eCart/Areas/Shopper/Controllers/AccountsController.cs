using eCart.Areas.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCart.Services;
using eCart.Models;
using System.Web.Security;

namespace eCart.Areas.Shopper.Controllers
{
    public class AccountsController : Controller
    {
        ecartdbContainer edb = new ecartdbContainer();
        //AccMgr accMgr = new AccMgr();
        StoreFactory storeFactory = new StoreFactory();

        //Authentication
        private readonly ecartdbContainer _dbContext = new ecartdbContainer();

        // User Roles
        private int ADMIN = 1;
        private int STORE_ADMIN = 2;
        private int STORE_MERCHANDISER = 3;
        private int SHOPPER = 4;
        private int RIDER = 5;

        // GET: Shopper/Accounts
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Login()
        //{
        //    return View();
        //}

        // Shopper/Accounts/Login
        //[HttpPost]
        //public ActionResult Login([Bind(Include ="Username, Password")] AccountLogin account )
        //{
        //    var accMgr = storeFactory.AccMgr;
        //    var result = accMgr.CheckLoginCredentials(account.Username, account.Password);

        //    if (result > 0)
        //    {
        //        Session["USERID"] = result;  
        //        Session["USER"] = account.Username;  
        //        CreateCart();

        //        return RedirectToAction("Index", "Home", new { area = "Shopper" });
        //    }
        //    else
        //    {
        //        Session["USERID"] = 1;
        //        Session["USER"] = "Admin";   //For test only
        //        CreateCart();

        //        return RedirectToAction("Index", "Home", new { area = "Shopper" });
        //    }
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

                    var accMgr = storeFactory.AccMgr;
                    var result = accMgr.CheckLoginCredentials(user.Username, user.Password);

                    Session["USERID"] = result;
                    Session["USER"] = user.Username;  
                    CreateCart();

                    return RedirectToAction("Index", "Home", new { area = "Shopper" });

                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }


        public ActionResult Logout()
        {
            Session["USER"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            ViewBag.UserStatusId = new SelectList(edb.UserStatus, "Id", "Name");

            ViewBag.MasterCityId = new SelectList(edb.MasterCities, "Id", "Name");

            ViewBag.UserStatusId = new SelectList(edb.MasterAreas, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "Name, Address, Email, Mobile, Password")] AccountRegistration registration)
        {
            if (registration != null )
            {
                var accMgr = storeFactory.AccMgr;

                //create user
                var userId = accMgr.CreateUser(registration.Email, registration.Password);

                if (userId != "Error")
                {
                    //assign user role
                    accMgr.SetUserRole(int.Parse(userId), SHOPPER);
             
                    registration.UserId = userId;
                    registration.UserStatusId = 1;
                    registration.MasterAreaId = 1;
                    registration.MasterCityId = 1;

                    //register account
                    accMgr.RegisterAccount(registration);

                    // proceed to login
                    return RedirectToAction("Login");
                }

            }

            ViewBag.UserStatusId = new SelectList(edb.UserStatus, "Id", "Name", 1);
            ViewBag.MasterCityId = new SelectList(edb.MasterCities, "Id", "Name", 1);
            ViewBag.UserStatusId = new SelectList(edb.MasterAreas, "Id", "Name", 1);
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