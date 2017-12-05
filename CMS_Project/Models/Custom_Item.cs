using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class Custom_Item
    {
        public int ID { set; get; }
        public item_lang ItemLang { set; get; }
        public int? ItemID { set; get; }
        public string Field_Key { set; get; }
        public string Field_Val { set; get; }
        public string Field_Type { set; get; }
    }
}