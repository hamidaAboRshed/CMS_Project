using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class PageTemplate
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string PageName { set; get; }
        public string Image { set; get; }
        public MenuItemType Type { set; get; }
    }
}