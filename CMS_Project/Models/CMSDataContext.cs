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
        public DbSet<item_lang> item_langs { get; set; }

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

            modelBuilder.Entity<Category_lang>()
        .HasKey(bc => new { bc.category_ID, bc.Lang_ID });

            modelBuilder.Entity<Category_lang>()
                .HasRequired<Category>(bc => bc.category)
                .WithMany()
                .HasForeignKey(bc => bc.category_ID);

            modelBuilder.Entity<Category_lang>()
                .HasRequired<Language>(bc => bc.Lang)
                .WithMany()
                .HasForeignKey(bc => bc.Lang_ID);

            //////////////////////

            modelBuilder.Entity<item_lang>()
        .HasKey(bc => new { bc.item_ID, bc.Lang_ID });

            modelBuilder.Entity<item_lang>()
                .HasRequired<ITEM>(bc => bc.item)
                .WithMany()
                .HasForeignKey(bc => bc.item_ID);

            modelBuilder.Entity<item_lang>()
                .HasRequired<Language>(bc => bc.Lang)
                .WithMany()
                .HasForeignKey(bc => bc.Lang_ID);

            //////////////////////

            modelBuilder.Entity<MenuItem_lang>()
        .HasKey(bc => new { bc.Menuitem_ID, bc.Lang_ID });

            modelBuilder.Entity<MenuItem_lang>()
                .HasRequired<MenuItem>(bc => bc.Menuitem)
                .WithMany()
                .HasForeignKey(bc => bc.Menuitem_ID);

            modelBuilder.Entity<MenuItem_lang>()
                .HasRequired<Language>(bc => bc.Lang)
                .WithMany()
                .HasForeignKey(bc => bc.Lang_ID);
        }

    }
}