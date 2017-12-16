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
            {
                var lang=db.Language.Single(x=>x.Default==true);
                List<item_lang> item = db.item_lang.Where(x => x.item.Cat_ID == id && x.Lang_ID.Value.Equals(lang.ID)).ToList();
                ViewBag.CatId = id;
                return View(item);
            }
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
        public JsonResult CreatePost(ITEM item)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                ITEM it=new ITEM
                {
                    Cat_ID=item.Cat_ID
                };

                foreach(var i in item.ItemLanguageList)
                {
                    it.ItemLanguageList.Add(i);
                }
               /* if (item.ImageFile != null && item.ImageFile.FileName != null && item.ImageFile.FileName != "")
                {
                    FileInfo fi = new FileInfo(item.ImageFile.FileName);
                    if (fi.Extension != ".jpeg" && fi.Extension != ".jpg" && fi.Extension != ".png" && fi.Extension != ".JPEG" && fi.Extension != ".JPG" && fi.Extension != ".PNG" )
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
                }*/
                
                db.ITEMs.Add(it);
                db.SaveChanges(); 
                status = true;
                //RedirectToAction("Index", "ITEM", new { id = it.Cat_ID });
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }


        //
        // GET: /ITEM/Edit/5

        public ActionResult Edit(int id = 0, int CatId=0)
        {
            item_lang item = db.item_lang.Find(id);
            List<Language> langlist = db.Language.ToList();
            ViewBag.langlist = new SelectList(langlist, "ID", "Name");
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
            if (ModelState.IsValid)
            {
               /* if (item.ImageFile != null && item.ImageFile.FileName != null && item.ImageFile.FileName != "")
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
                }*/
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                int CatID = Convert.ToInt32(TempData["Data"]);
                return RedirectToAction("Index", new { id = CatID });
            }
            return View(item);
        }

        //
        // GET: /ITEM/Translate/5

        public ActionResult Translate(int id = 0, int CatId = 0)
        {
            ViewBag.item_ID = id;
            ViewBag.CatID = CatId;
            return View();
        }

        //
        // POST: /ITEM/Translate/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Translate(item_lang item, string hiddenname)
        {
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
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                int CatID = item.item.Cat_ID;
                return RedirectToAction("Index", new { id = CatID });
            }
            return View(item);
        }

        //
        // GET: /ITEM/Delete/5

        public ActionResult Delete(int id = 0,int CatId=0)
        {
            var item = db.item_lang.Where(x => x.item_ID == id); 
            ViewBag.CatID = CatId;
            ViewBag.flag=false;
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
            var item_lan = db.item_lang.Where(x => x.item_ID == id); 
            ITEM item = db.ITEMs.Find(id);
            MenuItem MItem = db.MenuItems.SingleOrDefault(x => x.ItemId == id);
            if (MItem != null)
            {
                ViewBag.error= "This Item has reference from MenuItem, So You can not delete it";
                ViewBag.flag = true;
                ViewBag.CatID =item.Cat_ID;
                return View("Delete",item);
            }
            else
            {  
                int CatID = item.Cat_ID;
                db.item_lang.Remove((item_lang)item_lan);
                db.SaveChanges();
                db.ITEMs.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = CatID });
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}