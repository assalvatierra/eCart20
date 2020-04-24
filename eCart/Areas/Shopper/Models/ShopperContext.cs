using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eCart.Areas.Shopper.Models
{
    public class ShopperContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ShopperContext() : base("name=ShopperContext")
        {
        }

        public System.Data.Entity.DbSet<eCart.Models.CartDetail> CartDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.CartStatus> CartStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StoreDetail> StoreDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StorePickupPoint> StorePickupPoints { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.UserDetail> UserDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.MasterArea> MasterAreas { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.MasterCity> MasterCities { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StoreCategory> StoreCategories { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StoreStatus> StoreStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StoreItem> StoreItems { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.ItemMaster> ItemMasters { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.UserStatus> UserStatus { get; set; }
    }
}
