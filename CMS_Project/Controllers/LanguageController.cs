using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using CMS_Project.Models;

namespace CMS_Project.Controllers
{
    public class LanguageController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /Language/

        public ActionResult Index()
        {
            return View(db.Language.ToList());
        }

        //
        // GET: /Language/Details/5

        public ActionResult Details(int id = 0)
        {
            Language language = db.Language.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        //
        // GET: /Language/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Language/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Language language)
        {
            if (ModelState.IsValid)
            {
                if (language.Default == true)
                {
                    Language lang = db.Language.Where(x => x.Default == true).SingleOrDefault();
                    lang.Default = false;
                    db.Entry(lang).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Language.Add(language);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(language);
        }

        //
        // GET: /Language/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Language language = db.Language.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        //
        // POST: /Language/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Language language)
        {
            if (ModelState.IsValid)
            {
                if (language.Default == true)
                {
                    Language lang = db.Language.Where(x=>x.Default==true).SingleOrDefault();
                    lang.Default = false;
                    db.Entry(lang).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Entry(language).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(language);
        }

        //
        // GET: /Language/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Language language = db.Language.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        //
        // POST: /Language/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Language language = db.Language.Find(id);
            db.Language.Remove(language);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TranslateTerm(int id = 0)
        {
            ViewBag.lang_Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TranslateTerm(Dictionary Dic)
        {
            if (ModelState.IsValid)
            {
                int langId = Convert.ToInt32(TempData["Data"]);
                Dic.ID = langId;
                Language lang = db.Language.Find(langId);
                string lang_name = lang.Name;
                string xmlFilePath = "~/Content/XMLfile/"+lang_name+".xml";
                CreateXmlFile(xmlFilePath, Dic);
                return RedirectToAction("Index");
            }

            return View();
        }

        public void CreateXmlFile(string filepath, Dictionary Dic)
        {
            try
            {
                using (XmlWriter xmlwriter = XmlWriter.Create(Server.MapPath(filepath)))
                {
                    xmlwriter.WriteStartDocument();
                    xmlwriter.WriteStartElement("Dictionary");
                   // foreach (CustomerModel i in xmldata)
                   // {
                    Language lang = db.Language.Find(Dic.ID);
                    string lang_name = lang.Name;
                    xmlwriter.WriteStartElement(lang_name);
                        xmlwriter.WriteElementString("ID", Dic.ID.ToString());
                        xmlwriter.WriteElementString("SiteName", Dic.SiteName);
                        xmlwriter.WriteElementString("Language", Dic.Language);
                        xmlwriter.WriteElementString("Readmore", Dic.Readmore);
                        xmlwriter.WriteElementString("CopyRight", Dic.CopyRight);
                        xmlwriter.WriteEndElement();
                   // }
                    xmlwriter.WriteEndElement();
                    xmlwriter.WriteEndDocument();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Exception of type: " + ex + " occured please try again";
            }
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}