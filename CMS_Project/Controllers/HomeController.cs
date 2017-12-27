using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Project.Models;

namespace CMS_Project.Controllers
{
    public class HomeController : Controller
    {
        private CMSDataContext db = new CMSDataContext();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Session["LanguageId"] == null)
            {
                Session["LanguageId"] = 1;
            }
            return View();
        }

        public ActionResult ChangeLanguage(int id)
        {
            Session["LanguageId"] = id;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ListOfCategory(int id = 0, int TempId = 0)
        {
           // Category cat = new Category();
            //cat = db.Categories.Where(x => x.ID == id).FirstOrDefault();
            var cat = db.Categories.ToList();
            List<Category_lang> CatLangList=new List<Category_lang>();
            PageTemplate pageTemp = db.PageTemp.Find(TempId);
            if (pageTemp == null)
            {
                return View("PageNotFound");
            }
            else
            {
                int langId = Convert.ToInt32(Session["LanguageId"]);
                foreach (Category c in cat)
                {
                    var temp = db.Category_lang.Where(x => x.category_ID == c.ID && x.Lang_ID == langId).SingleOrDefault();
                    if (temp == null)
                    {
                        temp = db.Category_lang.Where(x => x.category_ID == c.ID && x.Lang_ID == 1).SingleOrDefault();
                    }
                    CatLangList.Add(temp);
                }
                return View(pageTemp.PageName, CatLangList);
            }
        }

        public ActionResult ListOfItem(int id = 0, int TempId = 0)
        {
            ViewBag.Cat_Id = id;
            var item = db.ITEMs.Where(x => x.Cat_ID == id).ToList();
            List<item_lang> ItemLangList = new List<item_lang>();
            PageTemplate pageTemp = db.PageTemp.Find(TempId);
            if (item == null || pageTemp == null)
            {
                return View("PageNotFound");
            }
            else
            {
                int langId = Convert.ToInt32(Session["LanguageId"]);
                foreach (ITEM itm in item)
                {
                    var temp = db.item_lang.Where(x => x.item_ID == itm.ID && x.Lang_ID == langId).SingleOrDefault();
                    if (temp == null)
                    {
                        temp = db.item_lang.Where(x => x.item_ID == itm.ID && x.Lang_ID == 1).SingleOrDefault();
                    }
                    ItemLangList.Add(temp);
                }
                return View(pageTemp.PageName, ItemLangList);
            }
        }
           
       


        public ActionResult ItemPerPage(int ID = 0,int TempId=0)
        {
            ITEM item = db.ITEMs.Find(ID);
            item_lang itemLang = new item_lang();
            PageTemplate pageTemp = db.PageTemp.Find(TempId);
            int langId = Convert.ToInt32(Session["LanguageId"]);
            if (item == null || pageTemp==null)
            {
                return View("PageNotFound");
            }
            else
            {
                itemLang = db.item_lang.SingleOrDefault(x => x.item_ID == ID && x.Lang_ID == langId);
                if (itemLang == null)
                {
                    itemLang = db.item_lang.SingleOrDefault(x => x.item_ID == ID && x.Lang_ID == 1);
                }
            }
            int itemID=itemLang.ID;
            int catID=(int)item.Cat_ID;
            itemLang.ItemCustomFieldList = db.Item_CustomField.Where(x => x.item_Id == itemID).ToList();
            itemLang.FieldList = db.Field.Where(x => x.ItemLangId == itemID).ToList();
            ViewBag.customField = db.Custom_Cat.Where(x => x.Cat_ID == catID).ToList();
            return View(pageTemp.PageName, itemLang);
        }
           
        



        public string Title { get; set; }
    }
}
