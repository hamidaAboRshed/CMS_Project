using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CMS_Project.Models;

namespace CMS_Project.Controllers
{
    public class UserController : Controller
    {
        private CMSDataContext db = new CMSDataContext();

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            List<Role> Rolelist = db.Role.ToList();
            ViewBag.Rolelist = new SelectList(Rolelist, "ID", "Name");
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            List<Role> Rolelist = db.Role.ToList();
            ViewBag.Rolelist = new SelectList(Rolelist, "ID", "Name");
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult login(Models.User x)
        {
            if (valid(x.username, x.password))
            {
                FormsAuthentication.SetAuthCookie(x.username, false);
                Response.Redirect("~/MenuItem");
            }
            else
            {
                ModelState.AddModelError("", "Username Or Password Is Wrong");
            }
            return View(x);
        }

        public bool valid(string username,string password)
        {
            bool isValid = false;
            var user = db.Users.FirstOrDefault(u => u.username == username);
            if (user != null)
            {
                if (user.password == password)
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        public void logout()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("login");
        }
    }
}