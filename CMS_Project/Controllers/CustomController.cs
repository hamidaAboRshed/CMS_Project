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
    public class CustomController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /Custom/

        public ActionResult Index(int id)
        {
            ViewBag.CatId = id;
            return View(db.Custom_Cat.Where(x=>x.Cat_ID==id).ToList());
        }

        //
        // GET: /Custom/Details/5

        public ActionResult Details(int id = 0)
        {
            Custom custom = db.Custom_Cat.Find(id);
            if (custom == null)
            {
                return HttpNotFound();
            }
            return View(custom);
        }

        //
        // GET: /Custom/Create

        public ActionResult Create(int id=0)
        {
            ViewBag.CatID = id;
            return View();
        }

        //
        // POST: /Custom/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Custom custom,string hiddenname)
        {
            if (ModelState.IsValid)
            {
                db.Custom_Cat.Add(custom);
                db.SaveChanges();
                return RedirectToAction("Index", "Custom", new { id = custom.Cat_ID });
            }

            //ViewBag.Cat_ID = new SelectList(db.Categories, "ID", "ID", custom.Cat_ID);
            return View(custom);
        }

        //
        // GET: /Custom/Edit/5

        public ActionResult Edit(int id = 0,int CatId =0)
        {
            Custom custom = db.Custom_Cat.Find(id);
            if (custom == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = CatId;
            return View(custom);
        }

        //
        // POST: /Custom/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Custom custom)//,string hiddenname)
        {
            if (ModelState.IsValid)
            {
                db.Entry(custom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID =  custom.Cat_ID;
            return View(custom);
        }

        //
        // GET: /Custom/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Custom custom = db.Custom_Cat.Find(id);
            if (custom == null)
            {
                return HttpNotFound();
            }
            return View(custom);
        }

        //
        // POST: /Custom/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Custom custom = db.Custom_Cat.Find(id);
            db.Custom_Cat.Remove(custom);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = custom.Cat_ID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}