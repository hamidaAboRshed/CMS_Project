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
            var lang = db.Language.Single(x => x.Default == true);
            List<MenuItem> menuitem = db.MenuItems.ToList();
            List<MenuItem_lang> ml = db.MenuItem_lang.Where(x => x.Lang_ID.Value.Equals(lang.ID)).ToList();
            return View(ml);
        }

        //
        // GET: /MenuItem/Details/5

        public ActionResult Details(int id = 0)
        {
            List<MenuItem> menuitem = db.MenuItems.ToList();
            MenuItem_lang menuitem_lang = db.MenuItem_lang.Find(id);
            if (menuitem == null)
            {
                return HttpNotFound();
            }
            return View(menuitem_lang);
        }

        //
        // GET: /MenuItem/Create

        public ActionResult Create()
        {
            var men = db.MenuItem_lang.ToList();
            ViewBag.parentlist = new SelectList(men, "Menuitem_ID", "Name");

            List<Language> languagelist = db.Language.ToList();
            ViewBag.langlist = new SelectList(languagelist, "ID", "Name");

            List<PageTemplate> templatelist = db.PageTemp.ToList();
            ViewBag.templates = new SelectList(templatelist, "ID", "Name");
            return View();
        }

        public JsonResult getCat(int langId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Category_lang> catlist = db.Category_lang.Where(x => x.Lang_ID == langId).ToList();
            return Json(catlist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getItem(int langId,int categoryId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<item_lang> listitem = db.item_lang.Where(x => x.Lang_ID == langId && x.item.Cat_ID == categoryId).ToList();
            return Json(listitem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getTemplate(MenuItemType type)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<PageTemplate> listTemp = db.PageTemp.Where(x => x.Type ==  type).ToList();
            return Json(listTemp, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        public JsonResult SaveMItem(MenuItem mi)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                PageTemplate p = db.PageTemp.Find(mi.Template_Id);
                MenuItem MenuItm = new MenuItem
                {
                    Order = mi.Order,
                    CatId = mi.CatId,
                    ItemId = mi.ItemId,
                    Parent_Id = mi.Parent_Id,
                    Visible = mi.Visible,
                    Type = mi.Type,
                    Template=p
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

        // GET: /MenuItem/Edit/5

        public ActionResult Edit(int id = 0,int MenuItem_Id=0)
        {
            List<MenuItem> menuitem = db.MenuItems.ToList();
            MenuItem_lang menuitem_lang = db.MenuItem_lang.Find(id);

            var lang = db.Language.Single(x => x.Default == true);
            List<MenuItem_lang> parentlist = db.MenuItem_lang.Where(x => x.Lang_ID.Value.Equals(lang.ID)).ToList();

            ViewBag.parentlist = new SelectList(parentlist, "Menuitem_ID", "Name",menuitem_lang.Menuitem.Parent_Id);

            List<PageTemplate> templatelist = db.PageTemp.ToList();
            ViewBag.templates = new SelectList(templatelist, "ID", "Name",menuitem_lang.Menuitem.Template.ID);

            List<Category_lang> categorylist = db.Category_lang.Where(x => x.Lang_ID.Value.Equals(lang.ID)).ToList();
            ViewBag.categorylist = new SelectList(categorylist, "category_ID", "Name", menuitem_lang.Menuitem.CatId);
            ViewBag.lang_Id = lang.ID;
            ViewBag.menuItem_Id = MenuItem_Id;
            ViewBag.Id = id;
            if (menuitem_lang == null)
            {
                return HttpNotFound();
            }
            return View(menuitem_lang);
        }

        //
        // POST: /MenuItem/Edit/5

        public JsonResult EditPost(MenuItem mi)
        {
            bool status = false;

                MenuItem m = db.MenuItems.Find(mi.ID);
                if (mi.CatId == 0)
                {
                    mi.CatId = m.CatId;
                }
                if (mi.ItemId == null)
                {
                    mi.ItemId = m.ItemId;
                }
                if (mi.Parent_Id == null)
                {
                    mi.Parent_Id = m.Parent_Id;
                }
                if (mi.Type == 0)
                {
                    mi.Type = m.Type;
                }
               /* if (mi.Visible == false)
                {
                    mi.Visible = m.Visible;
                }*/
                MenuItem_lang mi_lang = mi.MenuItemLanguageList[0];
                db.Entry(mi_lang).State = EntityState.Modified;
                db.Entry(m).CurrentValues.SetValues(mi);
                db.SaveChanges();
                status = true;
            return new JsonResult { Data = new { status = status } };
        }

        //
        // GET: /MenuItem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ViewBag.flag = false;
            MenuItem_lang mi_lang = db.MenuItem_lang.Find(id);
            MenuItem menuitem = db.MenuItems.Find(mi_lang.Menuitem_ID);

            MenuItem menuitem_son = db.MenuItems.SingleOrDefault(x => x.Parent_Id == menuitem.ID);
            if (menuitem_son != null)
            {
                ViewBag.error = "This MenuItem is a Perant to another MenuItem, So You can not delete it";
                ViewBag.flag = true;
            }
            if (menuitem == null)
            {
                return HttpNotFound();
            }
            return View(mi_lang);
        }

        //
        // POST: /MenuItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItem_lang menuitem = db.MenuItem_lang.Find(id);
            MenuItem menuitem_per = db.MenuItems.Find(menuitem.Menuitem_ID);
            db.MenuItems.Remove(menuitem_per);
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