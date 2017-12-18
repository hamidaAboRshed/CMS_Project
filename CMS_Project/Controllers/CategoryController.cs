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

        public ActionResult Index()
        {
            var lang = db.Language.Single(x => x.Default == true);
            List<Category_lang> category = db.Category_lang.Where(x => x.Lang_ID.Value.Equals(lang.ID)).ToList();
            return View(category);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category_lang category)
        {
            Category origin = new Category();
            if (ModelState.IsValid)
            {
                //origin.ID = category.ID;
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
                origin.Parent_Id = category.temp;
                db.Categories.Add(origin);
                db.SaveChanges();
                category.category_ID = origin.ID;
                db.Category_lang.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id=0)
        {
            Category_lang category = db.Category_lang.Find(id);
            Category cat_per = db.Categories.Find(category.category_ID);
            category.temp = cat_per.Parent_Id;

            List<Category_lang> men  = db.Category_lang.ToList();
            ViewBag.parentlist = new SelectList(men, "category_ID", "Name");


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
                Category_lang cat = db.Category_lang.Find(category.ID);
                category.category_ID = cat.category_ID;
                category.Lang_ID = cat.Lang_ID;
                Category cat_per = db.Categories.Find(category.category_ID);
                cat_per.Parent_Id = category.temp;
                if (category.ImageFile != null )
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
                else
                {
                    category.Image = cat.Image;
                }
                //db.Entry(category).State = EntityState.Modified;
                db.Entry(cat).CurrentValues.SetValues(category);
                db.Entry(cat_per).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id=0)
        {
            ViewBag.flag = false;
            Category_lang category = db.Category_lang.Find(id);
            Category cat_per = db.Categories.Find(category.category_ID);

            Category cat = db.Categories.SingleOrDefault(x => x.Parent_Id == cat_per.ID);
            if (cat != null)
            {
                ViewBag.error = "This Category is a Perant to another Category, So You can not delete it";
                ViewBag.flag = true;
            }
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
            Category cat_per = db.Categories.Find(category.category_ID);

                db.Categories.Remove(cat_per);
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