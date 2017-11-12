using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class CMSDataContext : DbContext
    {
        public CMSDataContext() : base("CMSConnection") {
        }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ITEM> ITEMs { get; set; }

        public DbSet<User> Users { get; set; }

    }
}