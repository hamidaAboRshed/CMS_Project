﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS_Project.Models
{
    public class item_lang 
    {
        public item_lang()
        {
            FieldList = new List<Field>();
            ItemCustomFieldList = new List<ItemCustomField>();
        }
        public int ID { set; get; }
        public String Title { set; get; }
        [DataType(DataType.Html)]
        public string Content { set; get; }
        public string Image { set; get; } 
        public string alt { set; get; }
        public string Description { set; get; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { set; get; }
        public Language Lang { set; get; }
        public int? Lang_ID { set; get; }
        public ITEM item { set; get; }
        public int? item_ID { set; get; }
        [NotMapped]
        public int? temp { set; get; }
        public List<Field> FieldList { set; get; }
        public List<ItemCustomField> ItemCustomFieldList { set; get; } 
    } 
}