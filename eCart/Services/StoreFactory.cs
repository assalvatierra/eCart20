using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Interfaces;

namespace eCart.Services
{
    public class StoreFactory
    {
        iStoreMgr storemgr;
        iCartMgr cartmgr;

        public StoreFactory()
        {
            this.storemgr = new Services.StoreMgr();
            this.cartmgr = new Services.CartMgr();
        }

        public iStoreMgr StoreMgr
        {
            get { return this.storemgr; }

        }


    }
}