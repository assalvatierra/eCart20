using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Interfaces;
using eCart.Models;

namespace eCart.Services
{
    public class AccMgr : iAccMgr
    {
        ecartdbContainer db = new ecartdbContainer();

        public int CheckLoginCredentials(string username, string password)
        {
            try
            {
                var user = db.UserDetails.Where(s => s.Email == username).FirstOrDefault();

                if (user != null)
                {
                    //verify user by password,
                    //then return userID
                    return user.Id;
                }
                
                //invalid login
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public string GetUserName(int userId)
        {
            try
            {
                var user = db.UserDetails.Find(userId);

                return user.Name;
            }
            catch (Exception)
            {
                return "NULL";
            }
        }

        public void RegisterAccount(AccountRegistration newAccount)
        {
            try
            {
                UserDetail user = new UserDetail()
                {
                    UserId = newAccount.UserId,
                    Name = newAccount.Name,
                    Email = newAccount.Email,
                    Address = newAccount.Address,
                    Mobile = newAccount.Mobile,
                    MasterAreaId = newAccount.MasterAreaId,
                    MasterCityId = newAccount.MasterCityId,
                    Remarks= " ",
                    UserStatusId = newAccount.UserStatusId
                };

                db.UserDetails.Add(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CreateUser(string username, string password)
        {
            try
            {
                var newUser = new User
                {
                    Username = username,
                    Password = password
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                return newUser.Id.ToString();
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        public void SetUserRole(int userId, int roleId)
        {
            //try
            //{
            //    var userRoles = new UserRolesMapping { 
                    
            //        UserId = userId,
            //        RoleId = roleId
            //    };
            //    db.UserRolesMappings.Add(userRoles);
            //    db.SaveChanges();

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public UserDetail GetUserDetail(int userId)
        {
            if (db.Users.Find(userId) != null )
            {
                var user = db.Users.Find(userId);
                var userdetails = db.UserDetails.Find(user.Id);
                return userdetails;
            }

            var guestUser = new UserDetail
            {
               Name = "Guest",
               Address = "N/A",
               Email = "N/A,",
               MasterAreaId = 1,
               MasterCityId = 1,
               Mobile = "NA",
               Remarks = "NA",
               UserStatusId = 1,
               UserId = "0"
            };

            return guestUser;
        }

        public void RegisterStore(StoreRegistration newStore)
        {
            try
            {
                StoreDetail store = new StoreDetail()
                {
                    Id = 0,
                    LoginId = newStore.LoginId,
                    Name = newStore.Name,
                    Address = newStore.Address,
                    MasterAreaId = newStore.MasterAreaId,
                    MasterCityId = newStore.MasterCityId,
                    Remarks = newStore.Remarks,
                    StoreCategoryId = newStore.StoreCategoryId,
                    StoreStatusId = 1,
                };

                db.StoreDetails.Add(store);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}