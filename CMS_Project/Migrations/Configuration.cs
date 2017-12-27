namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CMS_Project.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CMS_Project.Models.CMSDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CMS_Project.Models.CMSDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.PageTemp.AddOrUpdate(
                p => p.Name,
              new PageTemplate { Name = "CatTemp1", PageName="CatView1",Type=MenuItemType.ListOfCategory,Image="" },
              new PageTemplate { Name = "CatTemp2", PageName="CatView2",Type=MenuItemType.ListOfCategory,Image="" },
              new PageTemplate { Name = "ItemListTemp1", PageName="ViewItem",Type=MenuItemType.ListOfItem,Image="" },
              new PageTemplate { Name = "ItemPerPageTemp1" , PageName = "ItemPerPage1", Type = MenuItemType.ItemPerPage, Image = "" },
              new PageTemplate { Name = "ItemPerPageTemp2", PageName = "ItemPerPage2", Type = MenuItemType.ItemPerPage, Image = "" }
              );

            context.Language.AddOrUpdate(
                p => p.Name,
              new Language { Name = "English", Default = true });

            context.Permession.AddOrUpdate(
                p => p.Action,
              new Permession { Page = "MI_Create", Action = "MI_Create" },
              new Permession { Page = "MI_Edite", Action = "MI_Edite" },
              new Permession { Page = "MI_Details", Action = "MI_Details" },
              new Permession { Page = "MI_Delete", Action = "MI_Delete" },
              new Permession { Page = "MI_Translate", Action = "MI_Translate" },
              new Permession { Page = "Cat_Create", Action = "Cat_Create" },
              new Permession { Page = "Cat_Edite", Action = "Cat_Edite" },
              new Permession { Page = "Cat_Details", Action = "Cat_Details" },
              new Permession { Page = "Cat_Delete", Action = "Cat_Delete" },
              new Permession { Page = "Cat_Add_Item", Action = "Cat_Add_Item" },
              new Permession { Page = "Cat_Translate", Action = "Cat_Translate" },
              new Permession { Page = "Item_Create", Action = "Item_Create" },
              new Permession { Page = "Item_Edite", Action = "Item_Edite" },
              new Permession { Page = "Item_Details", Action = "Item_Details" },
              new Permession { Page = "Item_Delete", Action = "Item_Delete" },
              new Permession { Page = "Item_Translate", Action = "Item_Translate" },
              new Permession { Page = "User_Create", Action = "User_Create" },
              new Permession { Page = "User_Edite", Action = "User_Edite" },
              new Permession { Page = "User_Details", Action = "User_Details" },
              new Permession { Page = "User_Delete", Action = "User_Delete" }
            );

            context.Role.AddOrUpdate(
                p => p.Name,
              new Role { Name = "Visitor" },
              new Role { Name = "Admin" },
              new Role { Name = "Data Entery" }
            );

            context.Role_Per.AddOrUpdate(
                p => p.ID,
              new Role_Per { Role_ID = 2, Per_ID = 1 },
              new Role_Per { Role_ID = 2, Per_ID = 2 },
              new Role_Per { Role_ID = 2, Per_ID = 3 },
              new Role_Per { Role_ID = 2, Per_ID = 4 },
              new Role_Per { Role_ID = 2, Per_ID = 5 },
              new Role_Per { Role_ID = 2, Per_ID = 6 },
              new Role_Per { Role_ID = 2, Per_ID = 7 },
              new Role_Per { Role_ID = 2, Per_ID = 8 },
              new Role_Per { Role_ID = 2, Per_ID = 9 },
              new Role_Per { Role_ID = 2, Per_ID = 10 },
              new Role_Per { Role_ID = 2, Per_ID = 11 },
              new Role_Per { Role_ID = 2, Per_ID = 12 },
              new Role_Per { Role_ID = 2, Per_ID = 13 },
              new Role_Per { Role_ID = 2, Per_ID = 14 },
              new Role_Per { Role_ID = 2, Per_ID = 15 },
              new Role_Per { Role_ID = 2, Per_ID = 16 },
              new Role_Per { Role_ID = 2, Per_ID = 17 },
              new Role_Per { Role_ID = 2, Per_ID = 18 },
              new Role_Per { Role_ID = 2, Per_ID = 19 },
              new Role_Per { Role_ID = 2, Per_ID = 20 },
              new Role_Per { Role_ID = 3, Per_ID = 6 },
              new Role_Per { Role_ID = 3, Per_ID = 7 },
              new Role_Per { Role_ID = 3, Per_ID = 8 },
              new Role_Per { Role_ID = 3, Per_ID = 9 },
              new Role_Per { Role_ID = 3, Per_ID = 10 },
              new Role_Per { Role_ID = 3, Per_ID = 11 },
              new Role_Per { Role_ID = 3, Per_ID = 12 },
              new Role_Per { Role_ID = 3, Per_ID = 13 },
              new Role_Per { Role_ID = 3, Per_ID = 14 },
              new Role_Per { Role_ID = 3, Per_ID = 15 },
              new Role_Per { Role_ID = 3, Per_ID = 16 }

            );
        }
    }
}
