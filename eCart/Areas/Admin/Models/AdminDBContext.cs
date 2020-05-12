using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace eCart.Areas.Admin.Models
{
    public class AdminDBContext : DbContext
    {
        public AdminDBContext()
         : base("name=ecartdbContainer")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<StoreDetail> StoreDetails { get; set; }
        public virtual DbSet<MasterCity> MasterCities { get; set; }
        public virtual DbSet<MasterArea> MasterAreas { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<ItemCatGroup> ItemCatGroups { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StoreStatus> StoreStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StoreCategory> StoreCategories { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.UserStatus> UserStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.ItemMaster> ItemMasters { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.CartDetail> CartDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.CartStatus> CartStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StorePickupPoint> StorePickupPoints { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StorePickupPartner> StorePickupPartners { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StorePayment> StorePayments { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StorePaymentStatus> StorePaymentStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StorePaymentType> StorePaymentTypes { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.RiderDetail> RiderDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.RiderStatus> RiderStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.PaymentDetail> PaymentDetails { get; set; }
    }
}