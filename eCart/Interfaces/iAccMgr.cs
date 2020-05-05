using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCart.Models;
namespace eCart.Interfaces
{
    interface iAccMgr
    {

        int checkLoginCredentials(string username, string password);
        string getUserName(int userId);

        void registerAccount(AccountRegistration newAccount);
    }
}
