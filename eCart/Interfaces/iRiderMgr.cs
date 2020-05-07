using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCart.Areas.Shopper.Models;
using eCart.Models;

namespace eCart.Interfaces
{
    interface iRiderMgr
    {

        void AddCartPayment(RiderCashDetail cashDetail);
        void AddDeliveryActivity(int id, int statusId);
        void AddCartHistory(int id, int statusId);

        string getLastestActivity(int id);
    }
}
