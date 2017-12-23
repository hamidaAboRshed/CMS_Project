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
    public class MenuItem_langController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /MenuItem_lang/

        public ActionResult Index(int id = 0)
        {
            List<Language> lang = db.Language.Where(x => x.Default == false).ToList();
            List<MenuItem_lang> menuitem = new List<MenuItem_lang>();
            foreach (Language obj in lang)
            {
                List<MenuItem_lang> menuLang = db.MenuItem_lang.Where(x => x.Lang_ID.Value.Equals(obj.ID) && x.Menuitem_ID == id).ToList();
                menuitem.AddRange(menuLang);
            }
            ViewBag.menID = id;
            return View(menuitem);
        }

        //
        // GET: /MenuItem_lang/Details/5

        public ActionResult Details(int id = 0)
        {
            MenuItem_lang menuitem_lang = db.MenuItem_lang.Find(id);
            if (menuitem_lang == null)
            {
                return HttpNotFound();
            }
            return View(menuitem_lang);
        }

        //
        // GET: /MenuItem_lang/Create

        public ActionResult Create(int id = 0)
        {
            ViewBag.menID = id;
            List<Language> lang = db.Language.Where(x => x.Default == false).ToList();
            List<Language> language = new List<Language>();
            foreach (Language obj in lang)
            {
                MenuItem_lang menuLang = db.MenuItem_lang.Where(x => x.Lang_ID.Value.Equals(obj.ID) && x.Menuitem_ID == id).SingleOrDefault();
                if (menuLang==null)
                    language.Add(obj);
            }
            ViewBag.Lang_ID = new SelectList(language, "ID", "Name");
            return View();
        }

        //
        // POST: /MenuItem_lang/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuItem_lang menuitem_lang, string hiddenname)
        {
            if (ModelState.IsValid)
            {
                db.MenuItem_lang.Add(menuitem_lang);
                db.SaveChanges();
                int menId = menuitem_lang.Menuitem_ID;
                return RedirectToAction("Index", "MenuItem_lang", new { id = menId });
            }
            return View(menuitem_lang);
        }

        //
        // GET: /MenuItem_lang/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MenuItem_lang menuitem_lang = db.MenuItem_lang.Find(id);
            if (menuitem_lang == null)
            {
                return HttpNotFound();
            }
            return View(menuitem_lang);
        }

        //
        // POST: /MenuItem_lang/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MenuItem_lang menuitem_lang)
        {
            MenuItem_lang mi = db.MenuItem_lang.Find(menuitem_lang.ID);
            menuitem_lang.Lang_ID = mi.Lang_ID;
            menuitem_lang.Menuitem_ID = mi.Menuitem_ID;
            if (ModelState.IsValid)
            {
                db.Entry(mi).CurrentValues.SetValues(menuitem_lang);
                db.SaveChanges();
                int men_Id = mi.Menuitem_ID;
                return RedirectToAction("Index", "MenuItem_lang", new { id = men_Id });
            }
            return View(menuitem_lang);
        }

        //
        // GET: /MenuItem_lang/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MenuItem_lang menuitem_lang = db.MenuItem_lang.Find(id);
            if (menuitem_lang == null)
            {
                return HttpNotFound();
            }
            return View(menuitem_lang);
        }

        //
        // POST: /MenuItem_lang/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItem_lang menuitem_lang = db.MenuItem_lang.Find(id);
            db.MenuItem_lang.Remove(menuitem_lang);
            db.SaveChanges();
            int men_Id = menuitem_lang.Menuitem_ID;
            return RedirectToAction("Index", new { id = men_Id });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}