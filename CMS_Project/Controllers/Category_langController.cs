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

        public ActionResult Index()
        {
            var category_lang = db.Category_lang.Include(c => c.Lang).Include(c => c.category);
            return View(category_lang.ToList());
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
            ViewBag.CatID = id;
            ViewBag.Lang_ID = new SelectList(db.Language, "ID", "Name");
            return View();
        }

        //
        // POST: /Category_lang/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category_lang category,string hiddenname)
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
                //category.category_ID
                db.Category_lang.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index","Category");
            }

            return View(category);
        }

        //
        // GET: /Category_lang/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category_lang category_lang = db.Category_lang.Find(id);
            if (category_lang == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lang_ID = new SelectList(db.Language, "ID", "Name", category_lang.Lang_ID);
            ViewBag.category_ID = new SelectList(db.Categories, "ID", "ID", category_lang.category_ID);
            return View(category_lang);
        }

        //
        // POST: /Category_lang/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category_lang category_lang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category_lang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Lang_ID = new SelectList(db.Language, "ID", "Name", category_lang.Lang_ID);
            ViewBag.category_ID = new SelectList(db.Categories, "ID", "ID", category_lang.category_ID);
            return View(category_lang);
        }

        //
        // GET: /Category_lang/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category_lang category_lang = db.Category_lang.Find(id);
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
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}