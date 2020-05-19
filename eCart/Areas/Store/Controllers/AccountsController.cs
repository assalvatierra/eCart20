using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eCart.Areas.Store.Models;
using eCart.Models;
using eCart.Services;
using Microsoft.Ajax.Utilities;

namespace eCart.Areas.Store.Controllers
{

    public class AccountsController : Controller
    {
        private StoreContext db = new StoreContext();
        private StoreFactory storeFactory = new StoreFactory();

        private int STOREADMIN = 2;

        //Authentication
        private readonly ecartdbContainer _dbContext = new ecartdbContainer();

        // GET: Shopper/Accounts
        public ActionResult Index()
        {
            return View();
        }

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
                var accMgr = storeFactory.AccMgr;

                if (accMgr.IsUserValid(user))
                {
                    var login = accMgr.GetUser(user.Username, user.Password);

                    if (accMgr.IsUserInRole(login.Id, STOREADMIN))
                    {
                        var store = storeFactory.StoreMgr.GetStoreDetailByLoginId(login.Id.ToString());
                        if (store != null)
                        {
                            FormsAuthentication.SetAuthCookie(user.Username, false);
                            Session["USER"] = user.Username;
                            Session["STOREID"] = store.Id;  

                            return RedirectToAction("Index", "Home", new { area = "Store", id = store.Id });
                        }
                    }
                }
            }

            ModelState.AddModelError("Password", "invalid Username or Password");
            return View();
        }


        public ActionResult Logout()
        {
            Session["CARTDETAILS"] = null;
            Session["USERID"] = null;
            Session["USER"] = null;
            Session["STOREID"] = null;
            return RedirectToAction("Login", "Accounts", new { area = "Store" });
        }

        public ActionResult Register()
        {
            //Sample data
            StoreRegistration store = new StoreRegistration()
            {
                Address = "Bangkal, Davao City",
                MasterAreaId = 1,
                MasterCityId = 1,
                Name = "Seven Eleven Bangkal",
                Password = "12345",
                Password2 = "12345",
                StoreCategoryId = 2,
                Username = "SevenEleven",

            };

            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name");
            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(StoreRegistration store)
        {

            if (validateRegistrationFields(store))
            {
                var accMgr = storeFactory.AccMgr;
                if (!accMgr.IsUserExists(store.Username))
                {
                    //create user for store
                    var userId = accMgr.CreateUser(store.Username, store.Password);
                    if (int.Parse(userId) > 0)
                    {
                        accMgr.SetUserRole(int.Parse(userId), STOREADMIN);
                            
                        store.LoginId = userId;
                        store.StoreStatusId = 1;
                         
                        //register store
                        var newStore = storeFactory.StoreMgr.RegisterStore(store);

                        if (newStore)
                        {
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid Store Details");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Username", "Username already exists");
                }
            }

            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name");
            return View();
        }

        public bool validateRegistrationFields(StoreRegistration store)
        {
            bool isValid = true;
            if (store.Password != store.Password2)
            {
                ModelState.AddModelError("Password", "Password does not match");
                isValid = false;
            }

            if (store.Username.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Username", "Username cannot be empty");
                isValid = false;
            }

            if (store.Password.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Password", "Password cannot be empty");
                isValid = false;
            }

            if (store.Password2.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Password2", "Password cannot be empty");
                isValid = false;
            }

            if (store.Name.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Name", "Store Name cannot be empty");
                isValid = false;
            }

            if (store.Address.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Address", "Address cannot be empty");
                isValid = false;
            }


            return isValid;
        }
    }
}