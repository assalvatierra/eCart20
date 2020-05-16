using eCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCart.Interfaces
{
    interface iAccDb
    {
        User GetUser(int id);
        User GetUser(string username, string password);
        List<User> GetUserList();
        List<UserRolesMapping> GetUserRoles(int id);

        bool AddUser(User user);
        bool AddUserRole(UserRolesMapping userRolesMapping);




    }
}
