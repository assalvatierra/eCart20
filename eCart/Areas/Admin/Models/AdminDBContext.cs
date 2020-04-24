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



    }
}