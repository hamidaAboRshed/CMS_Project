using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class Custom_Cat
    {
        public int ID { set; get; }
        public string Cust_Name { set; get; }
        public string Cust_Type { set; get; }
        public Category_lang CategoryLang { set; get; }
        public int? Cat_ID { set; get; }
        public Field Field { set; get; }
        public int? Field_ID { set; get; }
        public bool Requierd { set; get; }
    }
}