using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Areas.Store.Models;
using eCart.Interfaces;
using eCart.Models;

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
            //initialize storemgr
            this.storemgr = new Services.StoreMgr();
            this.storemgr.setDbLayer(new StoreDBLayer());

            this.cartmgr = new Services.CartMgr();
            this.cartmgr.setDbLayer(new CartDbLayer());

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