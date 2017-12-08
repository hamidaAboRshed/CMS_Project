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
            return View(db.Category_lang.ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int idLan = 0, int idCat = 0)
        {
            //Category category = db.Categories.Find(id);
            var category = db.Category_lang.Where(x => x.Lang_ID == idLan && x.category_ID == idCat);
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
               origin.ID = (int)category.category_ID;
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
                    db.Categories.Add(origin);
                    db.SaveChanges();
                    db.Category_lang.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int idLan = 0,int idCat=0)
        {
            var category = db.Category_lang.Where(x=>x.Lang_ID==idLan && x.category_ID==idCat);
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

        public ActionResult Delete(int id = 0)
        {
            Category category = db.Categories.Find(id);
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
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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