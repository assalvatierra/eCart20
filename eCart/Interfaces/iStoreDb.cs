using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCart.Models;

namespace eCart.Interfaces
{
    public interface iStoreDb
    {
        StoreDetail GetStoreDetails(int id);

        bool CreateStoreDetail(StoreDetail storeDetail);

        bool EditStoreDetail(StoreDetail storeDetail);

    }
}
