using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class User
    {
        public int ID { set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public string fullname { set; get; }
        public string email { set; get; }
        public bool active { set; get; }
    }
}