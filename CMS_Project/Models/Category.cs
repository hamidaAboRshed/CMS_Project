using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{ 
    public class Category
    {
        public Category()
        {
            CategoryLanguageList = new List<Category_lang>();
        }
        public int ID { set; get; }
        public Category Parent { set; get; }
        public int? Parent_Id { set; get; }
        public virtual ICollection<ITEM> ItemsList { set; get; }
        public List<Category_lang> CategoryLanguageList { set; get; }
    }
} 