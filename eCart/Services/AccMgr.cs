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
        AccDb adb = new AccDb();

        #region For Revision
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
                if (db.Users.Any(u=>u.Username.ToLower() == username.ToLower()))
                {
                    var user = db.Users.Where(u => u.Username.ToLower() == username.ToLower() && u.Password == password).FirstOrDefault();

                    //find user in userDetails
                    if (db.UserDetails.Any(s => s.UserId == user.Id.ToString()))
                    {
                        var userDetail = db.UserDetails.Where(s => s.UserId == user.Id.ToString()).FirstOrDefault();

                        //then return userID
                        return userDetail.Id;
                    }
                    else
                    {
                        return 0;
                    }
                }
                
                //invalid login
                return -1;
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
                    Remarks = " ",
                    UserStatusId = newAccount.UserStatusId
                };

                db.UserDetails.Add(user);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                //throw ex;
                return false;
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
        #endregion

        public bool IsUserValid(User user)
        {
            var userList = adb.GetUserList();
            if (userList.Any(u=>u.Username.ToLower() == user.Username.ToLower() && u.Password == user.Password))
            {
                return true;
            }
            return false;
        }

        public User GetUser(string username, string password)
        {
            try
            {
                return adb.GetUser(username, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsUserInRole(int userId, int roleId)
        {
            try
            {
                var userRoles = adb.GetUserRoles(userId);

                if (userRoles.Any(s=>s.RoleId == roleId))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsUserExists(string username)
        {
            try
            {
                var userList = adb.GetUserList();

                if(userList.Any(u=>u.Username.ToLower() == username.ToLower()))
                {
                    return true;
                }

                return false;
            }
            catch
            {
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

                var result = adb.AddUser(newUser);

                if (result)
                {
                    return newUser.Id.ToString();
                }

                return "0";

            }
            catch (Exception)
            {
                return "0";
            }
        }

        public bool SetUserRole(int userId, int roleId)
        {
            try
            {
                var userRoles = new UserRolesMapping
                {
                    UserId = userId,
                    RoleId = roleId
                };

                return adb.AddUserRole(userRoles);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}