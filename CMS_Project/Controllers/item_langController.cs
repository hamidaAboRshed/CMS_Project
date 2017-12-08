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
    public class item_langController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /item_lang/

        public ActionResult Index(int id=0)
        {
            ViewBag.CatId = id;
            var item_lang = db.item_lang.Include(i => i.Lang).Include(i => i.item).Where(x=>x.item.Cat_ID==id);
            return View(item_lang.ToList());
        }

        //
        // GET: /item_lang/Details/5

        public ActionResult Details(int id = 0)
        {
            item_lang item_lang = db.item_lang.Find(id);
            if (item_lang == null)
            {
                return HttpNotFound();
            }
            return View(item_lang);
        }

        //
        // GET: /item_lang/Create

        public ActionResult Create(int id=0)
        {
            ViewBag.CatID = id;
            ViewBag.Lang_ID = new SelectList(db.Language, "ID", "Name");
            ViewBag.item_ID = new SelectList(db.ITEMs, "ID", "ID");
            return View();
        }

        //
        // POST: /item_lang/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(item_lang item_lang)
        {
            if (ModelState.IsValid)
            {
                db.item_lang.Add(item_lang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Lang_ID = new SelectList(db.Language, "ID", "Name", item_lang.Lang_ID);
            ViewBag.item_ID = new SelectList(db.ITEMs, "ID", "ID", item_lang.item_ID);
            return View(item_lang);
        }

        //
        // GET: /item_lang/Edit/5

        public ActionResult Edit(int id = 0)
        {
            item_lang item_lang = db.item_lang.Find(id);
            if (item_lang == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lang_ID = new SelectList(db.Language, "ID", "Name", item_lang.Lang_ID);
            ViewBag.item_ID = new SelectList(db.ITEMs, "ID", "ID", item_lang.item_ID);
            return View(item_lang);
        }

        //
        // POST: /item_lang/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(item_lang item_lang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item_lang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Lang_ID = new SelectList(db.Language, "ID", "Name", item_lang.Lang_ID);
            ViewBag.item_ID = new SelectList(db.ITEMs, "ID", "ID", item_lang.item_ID);
            return View(item_lang);
        }

        //
        // GET: /item_lang/Delete/5

        public ActionResult Delete(int id = 0)
        {
            item_lang item_lang = db.item_lang.Find(id);
            if (item_lang == null)
            {
                return HttpNotFound();
            }
            return View(item_lang);
        }

        //
        // POST: /item_lang/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            item_lang item_lang = db.item_lang.Find(id);
            db.item_lang.Remove(item_lang);
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