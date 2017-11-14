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
        [Key]
        public int ID { set; get; }
        public string Title { set; get; }
        [DataType(DataType.Html)]
        public string Content { set; get; }
        [DisplayName("Upload File")]
        public string Image { set; get; }
        public string Description { set; get; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { set; get; }

        public int Cat_ID { get; set; }

        public virtual Category CurrentCategory { get; set; } 
    }
}