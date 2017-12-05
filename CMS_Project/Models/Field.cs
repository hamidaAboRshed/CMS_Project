using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class Field
    {
        public int FieldID { set; get; }
        public string FieldVal { set; get; }
        public item_lang ItemLang { set; get; }
        public int? ItemId { set; get; }
    }
}