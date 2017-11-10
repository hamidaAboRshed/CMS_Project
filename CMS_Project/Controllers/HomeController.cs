using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Project.Models;

namespace CMS_Project.Controllers
{
    public class HomeController : Controller
    {
        private CMSDataContext db = new CMSDataContext();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CatView()
        {
           // Category cat = new Category();
            //cat = db.Categories.Where(x => x.ID == id).FirstOrDefault();
            return View();
        }

    }
}
