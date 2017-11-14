using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class ITEM
    {
        [Key]
        public int ID { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public string Image { set; get; }
        public string Description { set; get; }

        public int Cat_ID { get; set; }

        public virtual Category CurrentCategory { get; set; } 
    }
}