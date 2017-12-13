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
        public DbSet<Language> Language { get; set; }
        public DbSet<item_lang> item_lang { get; set; }
        public DbSet<Category_lang> Category_lang { get; set; }
        public DbSet<MenuItem_lang> MenuItem_lang { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<Custom> Custom_Cat { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permession> Permession { get; set; }
        public DbSet<Role_Per> Role_Per { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ITEM>()
            .HasRequired<Category>(s => s.CurrentCategory)
            .WithMany(g => g.ItemsList)
            .HasForeignKey<int>(s => s.Cat_ID); 
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
         
            ////////////////////////

           // modelBuilder.Entity<Category_lang>()
          //.HasKey(bc => new { bc.category_ID, bc.Lang_ID });

            modelBuilder.Entity<Category_lang>()
                .HasRequired<Category>(bc => bc.category)
                .WithMany(g => g.CategoryLanguageList)
                .HasForeignKey(bc => bc.category_ID);

            modelBuilder.Entity<Category_lang>()
                .HasRequired<Language>(bc => bc.Lang)
                .WithMany()
                .HasForeignKey(bc => bc.Lang_ID);

            //////////////////////

            //modelBuilder.Entity<item_lang>()
        //.HasKey(bc => new { bc.item_ID, bc.Lang_ID });

            modelBuilder.Entity<item_lang>()
                .HasRequired<ITEM>(bc => bc.item)
                .WithMany(g => g.ItemLanguageList)
                .HasForeignKey(bc => bc.item_ID);

            modelBuilder.Entity<item_lang>()
                .HasRequired<Language>(bc => bc.Lang)
                .WithMany()
                .HasForeignKey(bc => bc.Lang_ID);

            //////////////////////

            //modelBuilder.Entity<MenuItem_lang>()
        //.HasKey(bc => new { bc.Menuitem_ID, bc.Lang_ID });

            //modelBuilder.Entity<MenuItem_lang>()
              //  .HasRequired<MenuItem>(bc => bc.Menuitem)
              //  .WithMany()
              //  .HasForeignKey(bc => bc.Menuitem_ID);

            modelBuilder.Entity<MenuItem_lang>()
            .HasRequired<MenuItem>(s => s.Menuitem)
            .WithMany(g => g.MenuItemLanguageList)
            .HasForeignKey<int>(s => s.Menuitem_ID); 

            modelBuilder.Entity<MenuItem_lang>()
                .HasRequired<Language>(bc => bc.Lang)
                .WithMany()
                .HasForeignKey(bc => bc.Lang_ID);

            modelBuilder.Entity<MenuItem>().
            HasOptional(e => e.Parent).
            WithMany().
            HasForeignKey(m => m.Parent_Id);

            modelBuilder.Entity<Category>().
            HasOptional(e => e.Parent).
            WithMany().
            HasForeignKey(m => m.Parent_Id);

            /////////////////
            modelBuilder.Entity<Field>().
            HasOptional(e => e.ItemLang).
            WithMany().
            HasForeignKey(m => m.ItemId);

            modelBuilder.Entity<Custom>().
            HasOptional(e => e.CategoryLang).
            WithMany().
            HasForeignKey(m => m.Cat_ID);

            modelBuilder.Entity<Custom>().
            HasOptional(e => e.Field).
            WithMany().
            HasForeignKey(m => m.Field_ID);


            modelBuilder.Entity<Role_Per>().
            HasOptional(e => e.Role).
            WithMany().
            HasForeignKey(m => m.Role_ID);

            modelBuilder.Entity<Role_Per>().
            HasOptional(e => e.Permession).
            WithMany().
            HasForeignKey(m => m.Per_ID);

            modelBuilder.Entity<User>().
            HasOptional(e => e.Role).
            WithMany().
            HasForeignKey(m => m.Role_ID);




        }
    }
}