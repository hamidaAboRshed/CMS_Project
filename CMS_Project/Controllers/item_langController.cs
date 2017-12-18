using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Project.Models;
using System.IO;

namespace CMS_Project.Controllers
{
    public class item_langController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /item_lang/

        public ActionResult Index(int id = 0, int CatId = 0)
        {
            List<Language> lang = db.Language.Where(x => x.Default == false).ToList();
            List<item_lang> item=new List<item_lang>();
            foreach (Language obj in lang)
            {
               List<item_lang> itemLang = db.item_lang.Where(x => x.item.Cat_ID == CatId && x.Lang_ID.Value.Equals(obj.ID)).ToList();
               item.AddRange(itemLang);
            }
            ViewBag.CatId = CatId;
            ViewBag.ItemId = id;
            return View(item);
        }

        //
        // GET: /item_lang/Details/5

        public ActionResult Details(int id = 0, int CatId = 0)
        {
            item_lang item_lang = db.item_lang.Find(id);
            ViewBag.CatID = CatId;
            if (item_lang == null)
            {
                return HttpNotFound();
            }
            return View(item_lang);
        }

        //
        // GET: /item_lang/Create

        public ActionResult Create(int id = 0, int CatId = 0)
        {
            item_lang item = db.item_lang.Find(id);
            ViewBag.item_ID = item.item_ID;
            ViewBag.CatID = CatId;
            ViewBag.itemlang = id;

            ViewBag.Lang_ID = new SelectList(db.Language.Where(x=>x.Default==false), "ID", "Name");

            return View(item);
        }

        //
        // POST: /item_lang/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(item_lang item , string hiddenname)
        {
            if (ModelState.IsValid)
            {
                item_lang it = db.item_lang.Find(item.ID);
                ITEM item_per = db.ITEMs.Find(it.item_ID);
                if (item.ImageFile != null)
                {
                    FileInfo fi = new FileInfo(item.ImageFile.FileName);
                    if (fi.Extension != ".jpeg" && fi.Extension != ".jpg" && fi.Extension != ".png" && fi.Extension != ".JPEG" && fi.Extension != ".JPG" && fi.Extension != ".PNG")
                    {
                        TempData["Errormsg"] = "Image File Extension is Not valid";
                    }
                    else
                    {
                        string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
                        string extension = Path.GetExtension(item.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        item.Image = "~/Content/images/Item/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Content/images/Item/"), fileName);
                        item.ImageFile.SaveAs(fileName);
                    }
                }
                else
                {
                    item.Image = it.Image;
                }
                db.item_lang.Add(item);
                db.SaveChanges();
                int itemId = Convert.ToInt32(TempData["Data"]);
                int CatID = (int)item_per.Cat_ID;
                return RedirectToAction("Index", new { id = itemId, catId = CatID });
            }
            return View(item);
        }

        //
        // GET: /item_lang/Edit/5

        public ActionResult Edit(int id = 0, int CatId = 0 ,int itemlang=0)
        {
            item_lang item = db.item_lang.Find(id);
            ViewBag.CatID = CatId;
            ViewBag.itemlang = itemlang;
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
        [ValidateInput(false)]
        public ActionResult Edit(item_lang item, string hiddenname)
        {
            item_lang it = db.item_lang.Find(item.ID);
            item.Lang_ID = it.Lang_ID;
            if (ModelState.IsValid)
            {
                if (item.ImageFile != null && item.ImageFile.FileName != null && item.ImageFile.FileName != "")
                {
                    FileInfo fi = new FileInfo(item.ImageFile.FileName);
                    if (fi.Extension != ".jpeg" && fi.Extension != ".jpg" && fi.Extension != ".png" && fi.Extension != ".JPEG" && fi.Extension != ".JPG" && fi.Extension != ".PNG")
                    {
                        TempData["Errormsg"] = "Image File Extension is Not valid";
                    }
                    else
                    {
                        string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
                        string extension = Path.GetExtension(item.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        item.Image = "~/Content/images/Item/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Content/images/Item/"), fileName);
                        item.ImageFile.SaveAs(fileName);
                    }
                }
                else
                {
                    item.Image = it.Image;
                }
                //db.Entry(item).State = EntityState.Modified;
                db.Entry(it).CurrentValues.SetValues(item);
                db.SaveChanges();
                int itemId = Convert.ToInt32(TempData["message"]);
                int CatID = Convert.ToInt32(TempData["Data"]);
                return RedirectToAction("Index", new { id = itemId, catId = CatID });
            }
            return View(item);
        }

        //
        // GET: /item_lang/Delete/5

        public ActionResult Delete(int id = 0, int CatId = 0 ,int itemlang=0)
        {
            item_lang item_lang = db.item_lang.Find(id);
            ViewBag.itemlang = itemlang;
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
            ITEM item_per = db.ITEMs.Find(item_lang.item_ID);
            db.item_lang.Remove(item_lang);
            db.SaveChanges();
            int itemlang = Convert.ToInt32(TempData["Data"]);
            int CatID = (int)item_per.Cat_ID;
            return RedirectToAction("Index", new { id = itemlang, catId = CatID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}