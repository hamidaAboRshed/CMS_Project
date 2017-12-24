using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class ItemCustomField
    {
        public int ID { set; get; }
        public string FieldKey { set; get; }
        public FieldType FieldType { set; get; }
        public string FieldValue { set; get; }
        public ITEM CurrentItem { set; get; }
        public int? item_Id { set; get; }
    }
}