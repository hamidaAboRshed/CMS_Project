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
        public ActionResult ListOfCategory(int id=0)
        {
           // Category cat = new Category();
            //cat = db.Categories.Where(x => x.ID == id).FirstOrDefault();
            var cat = db.Categories.ToList();
            List<Category_lang> CatLangList=new List<Category_lang>();
            int langId=Convert.ToInt32(Session["LanguageId"]);
            foreach (Category c in cat)
            {
                var temp = db.Category_lang.Where(x => x.category_ID == c.ID && x.Lang_ID == langId).SingleOrDefault();
                if (temp == null)
                {
                    temp = db.Category_lang.Where(x => x.category_ID == c.ID && x.Lang_ID == 1).SingleOrDefault();
                }
                CatLangList.Add(temp);
            }
            return View("CatView", CatLangList);
        }

        public ActionResult ListOfItem(int id=0)
        {
            var item = db.ITEMs.Where(x => x.Cat_ID == id).ToList();
            List<item_lang> ItemLangList = new List<item_lang>();
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
            return View("viewItem", ItemLangList);
        }
           
       


        public ActionResult ItemPerPage(int ID = 0)
        {
            ITEM item = db.ITEMs.Find(ID);
            item_lang itemLang = new item_lang();
            int langId = Convert.ToInt32(Session["LanguageId"]);
            if (item == null)
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
            return View(itemLang);
        }
           
        



        public string Title { get; set; }
    }
}
