using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class ITEM
    {
        public ITEM()
        {
            ItemLanguageList = new List<item_lang>();
        }
        public int ID { set; get; }
          
        public int? Cat_ID { get; set; }

        public virtual Category CurrentCategory { get; set; }
        public List<item_lang> ItemLanguageList { set; get; }
        public List<ItemCustomField> ItemCustomFieldList { set; get; } 
    }
} 