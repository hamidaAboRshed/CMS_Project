using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class MenuItem
    {
        public MenuItem()
        {
            Template = new PageTemplate();
            MenuItemLanguageList = new List<MenuItem_lang>();
        }
        public int ID { set; get; }
        public int Order { set; get; }
        public MenuItem Parent { set; get; }
        public int? Parent_Id { set; get; } 
        public MenuItemType Type { set; get; } 
        public bool Visible { set; get; }
        public ITEM Item { set; get; }
        public int? ItemId { set; get; }
        public int CatId { set; get; }
        public PageTemplate Template { set; get; }
        public int? Template_Id { set; get; }
        public List<MenuItem_lang> MenuItemLanguageList { set; get; }
        
    }
} 