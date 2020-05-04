using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eCart.Areas.Shopper
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

        public System.Data.Entity.DbSet<eCart.Models.PaymentDetail> PaymentDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.CartDetail> CartDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.PaymentParty> PaymentParties { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.PaymentReceiver> PaymentReceivers { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.PaymentStatus> PaymentStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.UserDetail> UserDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.UserStatus> UserStatuses { get; set; }
    }
}
