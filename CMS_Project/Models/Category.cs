﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class Category
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public Category Parent { set; get; }
        public string Image { set; get; }
        public string Description { set; get; }
    }
}