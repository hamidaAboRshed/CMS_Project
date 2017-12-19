using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class Custom
    {
        public int ID { set; get; }
        public string Cust_Name { set; get; }
        public FieldType Cust_Type { set; get; }
        public Category Category { set; get; }
        public int? Cat_ID { set; get; }
        public bool Requierd { set; get; }
    }
}