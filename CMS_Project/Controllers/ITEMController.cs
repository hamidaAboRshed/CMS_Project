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
    public class ITEMController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /ITEM/

        public ActionResult Index()
        {
            return View(db.ITEMs.ToList());
        }

        //
        // GET: /ITEM/Details/5

        public ActionResult Details(int id = 0)
        {
            ITEM item = db.ITEMs.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        //
        // GET: /ITEM/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ITEM/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ITEM item)
        {
            if (ModelState.IsValid)
            {
                db.ITEMs.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        //
        // GET: /ITEM/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ITEM item = db.ITEMs.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        //
        // POST: /ITEM/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ITEM item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        //
        // GET: /ITEM/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ITEM item = db.ITEMs.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        //
        // POST: /ITEM/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ITEM item = db.ITEMs.Find(id);
            db.ITEMs.Remove(item);
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