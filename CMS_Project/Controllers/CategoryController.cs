using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Project.Models;
using System.Text;
using System.IO;

namespace CMS_Project.Controllers
{
    public class CategoryController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /Category/

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveMItem(MenuItem mi)
        {
            bool status = false;

            //mi.MenuItemLanguageList[0].Lang_ID = 1;
            if (ModelState.IsValid)
            {
                MenuItem MenuItm = new MenuItem
                {
                    Order = mi.Order,
                    ItemId = 1,
                    Visible = mi.Visible,
                    Type = mi.Type
                };
                foreach (var i in mi.MenuItemLanguageList)
                {
                    //
                    // i.TotalAmount = 
                    MenuItm.MenuItemLanguageList.Add(i);
                }
                db.MenuItems.Add(MenuItm);
                db.SaveChanges();
                status = true;

            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult Index()
        {
            
            //var tuple = new Tuple<Category, Category_lang>(new Category(), new Category_lang());
            //return View(tuple);
            //return View(db.Category_lang.ToList());
            var Category = db.Categories.ToList();
            var Category_lang = db.Category_lang.ToList();
            var allModels = new Tuple<List<Category>,
              List<Category_lang>>
              (Category, Category_lang) { };
            return View(allModels);
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id=0)
        {
            Category_lang category = db.Category_lang.Find(id);
            //var category = db.Category_lang.Where(x => x.Lang_ID == idLan && x.category_ID == idCat);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create

       //[HttpPost]
        public ActionResult Create()
        {
            var men = db.Category_lang.ToList();
            ViewBag.parentlist = new SelectList(men, "category_ID", "Name");

            List<Language> langlist = db.Language.ToList();
            ViewBag.langlist = new SelectList(langlist, "ID", "Name");
            return View();
        }

        //
        // POST: /Category/Create
        [HttpPost]
        public JsonResult CreatePost(Category category)
        {
            bool status = false;
       
         if (ModelState.IsValid)
           {
               Category cat = new Category
               {
                   Parent_Id=category.Parent_Id
               };
               foreach (var i in category.CategoryLanguageList)
               {
                   cat.CategoryLanguageList.Add(i);
               }

               
              /* if (category.ImageFile != null && category.ImageFile.FileName != null && category.ImageFile.FileName != "")
               {
                   FileInfo fi = new FileInfo(category.ImageFile.FileName);
                   if (fi.Extension != ".jpeg" && fi.Extension != ".jpg" && fi.Extension != ".png" && fi.Extension != ".JPEG" && fi.Extension != ".JPG" && fi.Extension != ".PNG")
                   {
                       TempData["Errormsg"] = "Image File Extension is Not valid";
                   }
                   else
                   {
                       string fileName = Path.GetFileNameWithoutExtension(category.ImageFile.FileName);
                       string extension = Path.GetExtension(category.ImageFile.FileName);
                       fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                       category.Image = "~/Content/images/Cat/" + fileName;
                       fileName = Path.Combine(Server.MapPath("~/Content/images/Cat/"), fileName);
                       category.ImageFile.SaveAs(fileName);
                   }
               }*/
                   db.Categories.Add(cat);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    status = true;

           }
         else
         {
             status = false;
         }
         return new JsonResult { Data = new { status = status } };
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id=0)
        {
            Category_lang category = db.Category_lang.Find(id);
            //var category = db.Category_lang.Where(x=>x.Lang_ID==idLan && x.category_ID==idCat);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category_lang category)
        {
            if (ModelState.IsValid)
            {
                if (category.ImageFile != null && category.ImageFile.FileName != null && category.ImageFile.FileName != "")
                {
                    FileInfo fi = new FileInfo(category.ImageFile.FileName);
                    if (fi.Extension != ".jpeg" && fi.Extension != ".jpg" && fi.Extension != ".png" && fi.Extension != ".JPEG" && fi.Extension != ".JPG" && fi.Extension != ".PNG")
                    {
                        TempData["Errormsg"] = "Image File Extension is Not valid";
                    }
                    else
                    {
                        string fileName = Path.GetFileNameWithoutExtension(category.ImageFile.FileName);
                        string extension = Path.GetExtension(category.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        category.Image = "~/Content/images/Cat/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Content/images/Cat/"), fileName);
                        category.ImageFile.SaveAs(fileName);
                    }
                }
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id=0)
        {
            Category_lang category = db.Category_lang.Find(id);
            //var category = db.Category_lang.Where(x => x.Lang_ID == idLan && x.category_ID == idCat);
            if (category == null)
            {
                return HttpNotFound(); 
            }
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id=0)
        {
            Category_lang category = db.Category_lang.Find(id);
            //var category = db.Category_lang.Where(x => x.Lang_ID == idLan && x.category_ID == idCat);
            //db.Category_lang.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}