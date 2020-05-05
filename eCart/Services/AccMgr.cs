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

        public int checkLoginCredentials(string username, string password)
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

        public string getUserName(int userId)
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

        public void registerAccount(AccountRegistration newAccount)
        {
            try
            {
                UserDetail user = new UserDetail()
                {
                    UserId = "1",
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
    }
}