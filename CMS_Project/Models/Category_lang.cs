using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{ 
    public class Category_lang
    {
        public string Name { set; get; }
        [DisplayName("Upload File")]
        public string Image { set; get; } 
        public string Description { set; get; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { set; get; }
        public Language Lang { set; get; }
        public int? Lang_ID { set; get; }
        public Category category { set; get; }
        public int? category_ID { set; get; }
        
    } 
}