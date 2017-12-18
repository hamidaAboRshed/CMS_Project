using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Project.Models;
using System.Text;
using System.IO;

namespace CMS_Project.Controllers
{
    public class ITEMController : Controller
    {
        private CMSDataContext db = new CMSDataContext();
        //
        // GET: /ITEM/

       /* public ActionResult Index()
        {
            return View(db.ITEMs.ToList());
        }*/
        
        public ActionResult Index(int id=0)
        {
            var lang=db.Language.Single(x=>x.Default==true);
            List<item_lang> item = db.item_lang.Where(x => x.item.Cat_ID == id && x.Lang_ID.Value.Equals(lang.ID)).ToList();
            ViewBag.CatId = id;
            return View(item);
            
        }
        //
        // GET: /ITEM/Details/5

        public ActionResult Details(int id = 0,int CatId=0)
        {
            item_lang item = db.item_lang.Find(id); 
            ViewBag.CatID = CatId;
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        //
        // GET: /ITEM/Create

        public ActionResult Create(int id=0)
        {
            ViewBag.CatID = id;
            List<Language> langlist = db.Language.ToList();
            ViewBag.langlist = new SelectList(langlist, "ID", "Name");
            return View();
        }

        //
        // POST: /ITEM/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(item_lang item, string hiddenname)
        {
            ITEM item_per = new ITEM();
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
                item_per.Cat_ID = item.temp;
                db.ITEMs.Add(item_per);
                db.SaveChanges();
                item.item_ID = item_per.ID;
                db.item_lang.Add(item);
                db.SaveChanges();
                int CatID = (int)item_per.Cat_ID;
                return RedirectToAction("Index", "ITEM", new { id = CatID });
            }

            return View(item);
        }


        //
        // GET: /ITEM/Edit/5

        public ActionResult Edit(int id = 0, int CatId=0)
        {
            item_lang item = db.item_lang.Find(id);
            ViewBag.CatID = CatId;
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
                int CatID = Convert.ToInt32(TempData["Data"]);
                return RedirectToAction("Index", new { id = CatID });
            }
            return View(item);
        }


        //
        // GET: /ITEM/Delete/5

        public ActionResult Delete(int id = 0,int CatId=0)
        {
            item_lang item = db.item_lang.Find(id); 
            ViewBag.CatID = CatId;
            ViewBag.flag=false;

            ITEM item_per = db.ITEMs.Find(item.item_ID);
            MenuItem MItem = db.MenuItems.SingleOrDefault(x => x.ItemId == item_per.ID);
            if (MItem != null)
            {
                ViewBag.error = "This Item has reference from MenuItem, So You can not delete it";
                ViewBag.flag = true;
            }
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
            item_lang item_lan = db.item_lang.Find(id);
            ITEM item = db.ITEMs.Find(item_lan.item_ID);
     
                int CatID = (int)item.Cat_ID;
                db.ITEMs.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = CatID });
            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}