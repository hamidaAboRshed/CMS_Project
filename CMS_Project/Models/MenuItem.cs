using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}