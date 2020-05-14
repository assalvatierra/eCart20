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

        public bool VerifyUserRole(User user, int roleId)
        {

                var userLogin = db.Users.Where(u => u.Username.ToLower() == user.Username.ToLower() && u.Password == user.Password).FirstOrDefault();
                var isUserInRole = db.UserRolesMappings.Where(r => r.UserId == userLogin.Id && r.RoleId == roleId).FirstOrDefault();

                if (db.UserRolesMappings.Any(r => r.UserId == userLogin.Id && r.RoleId == roleId))
                {
                    return true;
                }

                //invalid login
                return false;

        }

        public int CheckLoginCredentials(string username, string password)
        {
            try
            {
                //verify user by password
                if (db.Users.Any(u=>u.Username == username && u.Password == password))
                {
                    var user = db.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
                    var userDetail = db.UserDetails.Where(s => s.UserId == user.Id.ToString()).FirstOrDefault();

                    //then return userID
                    return userDetail.Id;
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

        public bool RegisterAccount(AccountRegistration newAccount)
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

                return true;
            }
            catch (Exception )
            {
                //throw ex;
                return false;
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
            try
            {
                var userRoles = new UserRolesMapping
                {

                    UserId = userId,
                    RoleId = roleId
                };
                db.UserRolesMappings.Add(userRoles);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public void RegisterRider(RiderRegistration newRider)
        {
            try
            {
                RiderDetail rider = new RiderDetail()
                {
                    Id = 0,
                    UserId = newRider.UserId,
                    Name = newRider.Name,
                    Address = newRider.Address,
                    Mobile = newRider.Mobile,
                    Mobile2 = newRider.Mobile2,
                    MasterCityId = newRider.MasterCityId,
                    Remarks = newRider.Remarks,
                    RiderStatusId = 1,
                };

                db.RiderDetails.Add(rider);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}