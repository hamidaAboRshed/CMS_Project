using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Project.Models;

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

        public ActionResult Create()
        {
            ViewBag.Lang_ID = new SelectList(db.Language, "ID", "Name");
            return View();
        }

        //
        // POST: /Category_lang/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category_lang category_lang)
        {
            if (ModelState.IsValid)
            {
                db.Category_lang.Add(category_lang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category_lang);
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