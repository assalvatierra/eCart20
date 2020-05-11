using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eCart.Areas.Store.Models;
using eCart.Models;
using eCart.Services;

namespace eCart.Areas.Store.Controllers
{

    public class AccountsController : Controller
    {
        private StoreContext db = new StoreContext();
        private StoreFactory storeFactory = new StoreFactory();

        //Authentication
        private readonly ecartdbContainer _dbContext = new ecartdbContainer();

        // GET: Shopper/Accounts
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Login() {

        //    var storeTest = db.StoreDetails.FirstOrDefault();

        //    ViewBag.username = "";

        //    return View();
        //}

        //// Store/Accounts/Login
        //[HttpPost]
        //public ActionResult Login( string username, string password )
        //{
        //    Session["STOREID"] = "1";   //For test only
        //    var LoginId = 1;

        //    return RedirectToAction("Index", "Home", new { area = "Store", id = LoginId });
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

                    var login = _dbContext.Users.Where(u => u.Username.ToLower() == user.Username.ToLower() && u.Password == user.Password).FirstOrDefault();
                    var store = _dbContext.StoreDetails.Where(s => s.LoginId == login.Id.ToString()).FirstOrDefault();

                    //Session["STOREID"] = store.Id.ToString();  
                    //var LoginId = store.Id;

                    Session["STOREID"] = 1;   //For test only
                    var LoginId = 1;          //For test only

                    return RedirectToAction("Index", "Home", new { area = "Store", id = LoginId });

                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }


        public ActionResult Logout()
        {
            Session["STOREID"] = null;
            return RedirectToAction("Login", "Accounts", new { area = "Store" });
        }

        public ActionResult Register()
        {
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Register(StoreRegistration store)
        {

            var accMgr = storeFactory.AccMgr;

            if (accMgr.CheckLoginCredentials(store.Email, store.Password) > 0)
            {
                //create user for store
                var userId = accMgr.CreateUser(store.Email, store.Password);
                store.LoginId = userId; 

                //register store
                accMgr.RegisterStore(store);
                return RedirectToAction("Login");
            }

            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name");
            return View();
        }
    }
}