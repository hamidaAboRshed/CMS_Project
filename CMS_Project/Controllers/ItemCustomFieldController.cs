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
    public class ItemCustomFieldController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /ItemCustomField/

        public ActionResult Index(int id=0)
        {
            var item_customfield = db.Item_CustomField.Where(i => i.item_Id==id);
            ViewBag.item_Id = id;
            return View(item_customfield.ToList());
        }

        //
        // GET: /ItemCustomField/Details/5

        public ActionResult Details(int id = 0)
        {
            ItemCustomField itemcustomfield = db.Item_CustomField.Find(id);
            if (itemcustomfield == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_Id = itemcustomfield.item_Id;
            return View(itemcustomfield);
        }

        //
        // GET: /ItemCustomField/Create

        public ActionResult Create(int id=0)
        {
            //ViewBag.item_Id = new SelectList(db.ITEMs, "ID", "ID");
            ViewBag.item_Id = id;
            return View();
        }

        //
        // POST: /ItemCustomField/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCustomField itemcustomfield, string hiddenname)
        {
            if (ModelState.IsValid)
            {
                db.Item_CustomField.Add(itemcustomfield);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = itemcustomfield.item_Id });
            }

            ViewBag.item_Id = itemcustomfield.item_Id;
            return View(itemcustomfield);
        }

        //
        // GET: /ItemCustomField/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ItemCustomField itemcustomfield = db.Item_CustomField.Find(id);
            if (itemcustomfield == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_Id = itemcustomfield.item_Id;
            return View(itemcustomfield);
        }

        //
        // POST: /ItemCustomField/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemCustomField itemcustomfield)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemcustomfield).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {id= itemcustomfield.item_Id });
            }
            ViewBag.item_Id =  itemcustomfield.item_Id;
            return View(itemcustomfield);
        }

        //
        // GET: /ItemCustomField/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ItemCustomField itemcustomfield = db.Item_CustomField.Find(id);
            if (itemcustomfield == null)
            {
                return HttpNotFound();
            }
            return View(itemcustomfield);
        }

        //
        // POST: /ItemCustomField/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemCustomField itemcustomfield = db.Item_CustomField.Find(id);
            db.Item_CustomField.Remove(itemcustomfield);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = itemcustomfield.item_Id });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}