using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class MenuItem_lang
    {
        public string Name { set; get; }
        public MenuItem Menuitem { set; get; } 
        public int? Menuitem_ID { set; get; }
        public Language Lang {set; get;}
        public int? Lang_ID { set; get; } 
       
    } 
}