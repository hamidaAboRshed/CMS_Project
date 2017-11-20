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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ITEM>()
            .HasRequired<Category>(s => s.CurrentCategory)
            .WithMany(g => g.ItemsList)
            .HasForeignKey<int>(s => s.Cat_ID); 
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
         
            modelBuilder.Entity<MenuItem>().
            HasOptional(e => e.Parent).
            WithMany().
            HasForeignKey(m => m.Parent_Id);
        }
    }
}