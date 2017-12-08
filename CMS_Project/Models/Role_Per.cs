using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class Role_Per
    {
        public int ID { set; get; }
        public Role Role { set; get; }
        public int? Role_ID { set; get; }
        public Permession Permession { set; get; }
        public int? Per_ID { set; get; }
    }
}