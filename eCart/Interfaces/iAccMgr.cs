﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCart.Interfaces
{
    interface iAccMgr
    {
        int checkLoginCredentials(string username, string password);
        string getUserName(int userId);

    }
}