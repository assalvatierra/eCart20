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
    }
}