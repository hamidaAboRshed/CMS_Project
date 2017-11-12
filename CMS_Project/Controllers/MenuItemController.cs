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
    public class MenuItemController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /MenuItem/

        public ActionResult Index()
        {
            return View(db.MenuItems.ToList());
        }

        //
        // GET: /MenuItem/Details/5

        public ActionResult Details(int id = 0)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            if (menuitem == null)
            {
                return HttpNotFound();
            }
            return View(menuitem);
        }

        //
        // GET: /MenuItem/Create


        public ActionResult Create()
        {
            
            //var men = db.MenuItems.ToList();
            //SelectList list = new SelectList(men, "ID", "Name",1);
            //ViewBag.menulist = list;
            //var menuitem = new MenuItem();
           // ViewBag.ActionStatus = new SelectList(men, "ID", "Name", menuitem.ID);

            /*var menuitem = new MenuItem();
            var men = db.MenuItems.ToList();
            menuitem.collection = new SelectList(men, "ID", "Name");*/
            return View();
        }

        //
        // POST: /MenuItem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuItem menuitem)
        {
            if (ModelState.IsValid)
            {
                db.MenuItems.Add(menuitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menuitem);
        }

        //
        // GET: /MenuItem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            if (menuitem == null)
            {
                return HttpNotFound();
            }
            return View(menuitem);
        }

        //
        // POST: /MenuItem/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MenuItem menuitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuitem);
        }

        //
        // GET: /MenuItem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            if (menuitem == null)
            {
                return HttpNotFound();
            }
            return View(menuitem);
        }

        //
        // POST: /MenuItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            db.MenuItems.Remove(menuitem);
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