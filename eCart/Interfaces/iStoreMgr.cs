using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCart.Interfaces
{
    interface iStoreMgr
    {
        int getStoreDetails(int id);
        int getStoreItems(int id);
        int getPickupPoint(int id);


    }
}
