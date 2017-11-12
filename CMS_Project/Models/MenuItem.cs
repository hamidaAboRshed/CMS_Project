using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Project.Models
{
    public class MenuItem
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int Order { set; get; }
        public MenuItem Parent { set; get; }
        public MenuItemType Type { set; get; }
        public bool Visible { set; get; }
        //[NotMapped]
       // public SelectList collection { get; set; }
    }
}