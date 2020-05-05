using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace eCart.Areas.Store.Models
{
    public class StoreContext: DbContext
    {

        public StoreContext()
             : base("name=ecartdbContainer")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<StoreDetail> StoreDetails { get; set; }
        public virtual DbSet<StoreStatus> StoreStatus { get; set; }
        public virtual DbSet<StoreCategory> StoreCategories { get; set; }
        public virtual DbSet<StoreItem> StoreItems { get; set; }
        public virtual DbSet<StorePickupPoint> StorePickupPoints { get; set; }
        public virtual DbSet<StorePickupPartner> StorePickupPartners { get; set; }
        public virtual DbSet<StorePickupStatus> StorePickupStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.MasterCity> MasterCities { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.MasterArea> MasterAreas { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.CartDetail> CartDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.CartStatus> CartStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.UserDetail> UserDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.ItemMaster> ItemMasters { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.ItemMasterCategory> ItemMasterCategories { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.ItemCategory> ItemCategories { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.ItemCatGroup> ItemCatGroups { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.PaymentDetail> PaymentDetails { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.PaymentParty> PaymentParties { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.PaymentReceiver> PaymentReceivers { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.PaymentStatus> PaymentStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StorePayment> StorePayments { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StorePaymentStatus> StorePaymentStatus { get; set; }

        public System.Data.Entity.DbSet<eCart.Models.StorePaymentType> StorePaymentTypes { get; set; }
    }
}