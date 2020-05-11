using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace eCart.Models
{

    public class SecurityDBContext : DbContext
    {
        public SecurityDBContext()
            : base("name=ecartdbContainer")
        {
        }


        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRolesMapping> UserRolesMappings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
