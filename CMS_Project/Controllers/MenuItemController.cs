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

        [HttpPost]
        public JsonResult SaveMItem(MenuItem mi)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                MenuItem MenuItm = new MenuItem
                {
                    Order = mi.Order,
                    CatId=mi.CatId,
                    ItemId=mi.ItemId,
                    Parent_Id=mi.Parent_Id,
                    Visible = mi.Visible,
                    Type = mi.Type
                };
                foreach (var i in mi.MenuItemLanguageList)
                {
                    //
                    // i.TotalAmount = 
                    MenuItm.MenuItemLanguageList.Add(i);
                }
                db.MenuItems.Add(MenuItm);
                db.SaveChanges();
                status = true;

            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
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
            var men = db.MenuItem_lang.ToList();
            ViewBag.parentlist = new SelectList(men, "Menuitem_ID", "Name");


            List<Category_lang> categorylist = db.Category_lang.ToList();
            ViewBag.categorylist = new SelectList(categorylist, "category_ID", "Name");

            List<Language> languagelist = db.Language.ToList();
            ViewBag.langlist = new SelectList(languagelist, "ID", "Name");
            return View();
        }

        public JsonResult getItem(int categoryId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<item_lang> listitem=null;
            int item = db.ITEMs.Where(x => x.Cat_ID == categoryId).Select(p => p.ID).SingleOrDefault() ;
            if (item !=null)
            {
                listitem = db.item_lang.Where(x => x.item_ID == item).ToList();
            }
            return Json(listitem, JsonRequestBehavior.AllowGet);
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
            var men = db.MenuItems.ToList();
            ViewBag.parentlist = new SelectList(men, "ID", "Name");

            List<Category> categorylist = db.Categories.ToList();
            ViewBag.categorylist = new SelectList(categorylist, "ID", "Name");
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