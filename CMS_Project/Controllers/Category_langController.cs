using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Project.Models;
using System.IO;

namespace CMS_Project.Controllers
{
    public class Category_langController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /Category_lang/

        public ActionResult Index(int id=0)
        {
            List<Language> lang = db.Language.Where(x => x.Default == false).ToList();
            List<Category_lang> category = new List<Category_lang>();
            foreach (Language obj in lang)
            {
                List<Category_lang> CatLang = db.Category_lang.Where(x => x.Lang_ID.Value.Equals(obj.ID)).ToList();
                category.AddRange(CatLang);
            }
            ViewBag.Cat_lang = id;
            return View(category);
        }

        //
        // GET: /Category_lang/Details/5

        public ActionResult Details(int id = 0)
        {
            Category_lang category_lang = db.Category_lang.Find(id);
            if (category_lang == null)
            {
                return HttpNotFound();
            }
            return View(category_lang);
        }

        //
        // GET: /Category_lang/Create

        public ActionResult Create(int id = 0)
        {
            Category_lang category_lang = db.Category_lang.Find(id);
            ViewBag.CatID = category_lang.category_ID;
            
            ViewBag.Lang_ID = new SelectList(db.Language.Where(x=>x.Default==false), "ID", "Name");
            ViewBag.catlang = id;
            return View(category_lang);
        }

        //
        // POST: /Category_lang/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category_lang category, string hiddenname)
        {
            if (ModelState.IsValid)
            {
                Category_lang cat = db.Category_lang.Find(category.ID);
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
                //category.category_ID
                db.Category_lang.Add(category);
                db.SaveChanges();
                int CatId = Convert.ToInt32(TempData["Data"]);
                return RedirectToAction("Index", "Category_lang", new {  id = CatId });
            }

            return View(category);
        }

        //
        // GET: /Category_lang/Edit/5

        public ActionResult Edit(int id = 0,int catlang=0)
        {
            Category_lang category = db.Category_lang.Find(id);
            ViewBag.catlang = catlang;
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
        
                if (category.ImageFile != null)
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
                db.SaveChanges();
                int CatId = Convert.ToInt32(TempData["Data"]);
                return RedirectToAction("Index", "Category_lang", new { id = CatId });
            }
            return View(category);
        }


        //
        // GET: /Category_lang/Delete/5

        public ActionResult Delete(int id = 0, int catlang = 0)
        {
            Category_lang category_lang = db.Category_lang.Find(id);
            ViewBag.catlang = catlang;
            if (category_lang == null)
            {
                return HttpNotFound();
            }
            return View(category_lang);
        }

        //
        // POST: /Category_lang/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category_lang category_lang = db.Category_lang.Find(id);
            db.Category_lang.Remove(category_lang);
            db.SaveChanges();
            int catlang = Convert.ToInt32(TempData["Data"]);
            return RedirectToAction("Index", new { id = catlang });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}