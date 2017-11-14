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
                var item = (from d in db.ITEMs
                           where d.ID == id
                           select d).ToList();
                ViewBag.CatId = id;
                return View(item);
            }
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

        public ActionResult Create(int id=0)
        {
            ViewData["Cat_Id"] = id;
            return View();
        }

        //
        // POST: /ITEM/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ITEM item,string hiddenname)
        {

            if (ModelState.IsValid)
            {
                if (item.ImageFile != null && item.ImageFile.FileName != null && item.ImageFile.FileName != "")
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
                }
                db.ITEMs.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index",item.Cat_ID);
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