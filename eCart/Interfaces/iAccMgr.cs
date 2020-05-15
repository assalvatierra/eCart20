using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCart.Areas.Shopper.Models;
using eCart.Models;

namespace eCart.Interfaces
{
    public interface iAccMgr
    {
        bool VerifyUserRole(User user, int roleId);
        int CheckLoginCredentials(string username, string password);
        string GetUserName(int userId);
        UserDetail GetUserDetail(int userId);

        bool RegisterAccount(AccountRegistration newAccount);
        void RegisterStore(StoreRegistration newStore);
        void RegisterRider(RiderRegistration newRider);

        string CreateUser(string username, string password);
        void SetUserRole(int userId, int roleId);

        User GetUser(string username, string password);
        bool IsUserValid(User user);
        bool IsUserInRole(int userId, int roleId);

    }
}
