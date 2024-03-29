﻿using System;
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

        bool RegisterAccount(AccountRegistration newAccount);
        void RegisterRider(RiderRegistration newRider);

        string CreateUser(string username, string password);
        bool SetUserRole(int userId, int roleId);

        User GetUser(string username, string password);
        bool IsUserValid(User user);
        bool IsUserInRole(int userId, int roleId);
        bool IsUserExists(string username);

    }
}
