using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using eCart.Interfaces;
using eCart.Models;

namespace eCart.Models
{
    public class AccDb : iAccDb
    {
        ecartdbContainer db = new ecartdbContainer();

        public User GetUser(int id)
        {
            return db.Users.Find(id);
        }

        public User GetUser(string username, string password)
        {
            return db.Users.Where(u => u.Username.ToLower() == username.ToLower() && u.Password == password).FirstOrDefault();
        }

        public List<User> GetUserList()
        {
            return db.Users.ToList();
        }

        public List<UserRolesMapping> GetUserRoles(int id)
        {
            if (db.UserRolesMappings.Any(s=>s.UserId == id))
            {
                return db.UserRolesMappings.Where(s => s.UserId == id).ToList();
            }

            return null;
        }
    }
}