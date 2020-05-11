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

        int CheckLoginCredentials(string username, string password);
        string GetUserName(int userId);
        UserDetail GetUserDetail(int userId);

        void RegisterAccount(AccountRegistration newAccount);
        void RegisterStore(StoreRegistration newStore);

        string CreateUser(string username, string password);
        void SetUserRole(int userId, int roleId);
    }
}
