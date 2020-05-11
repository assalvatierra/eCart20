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
        iRiderMgr riderMgr;
        iAccMgr accMgr;

        public StoreFactory()
        {
            this.storemgr = new Services.StoreMgr();
            this.cartmgr = new Services.CartMgr();
            this.accMgr = new Services.AccMgr();
            this.riderMgr = new Services.RiderMgr();
        }

        public iStoreMgr StoreMgr
        {
            get { return this.storemgr; }

        }

        public iCartMgr CartMgr
        {
            get { return this.cartmgr; }
        }


        public iAccMgr AccMgr
        {
            get { return this.accMgr; }
        }

        public iRiderMgr RiderMgr
        {
            get { return this.riderMgr; }
        }

    }
}