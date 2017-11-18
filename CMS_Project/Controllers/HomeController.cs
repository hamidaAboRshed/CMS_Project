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
        public ActionResult ListOfCategory(int id=0)
        {
           // Category cat = new Category();
            //cat = db.Categories.Where(x => x.ID == id).FirstOrDefault();
            return View("CatView");
        }

        public ActionResult ListOfItem(int id=0)
        {
            var item = db.ITEMs.Where(x => x.Cat_ID == id).ToList();
            return View("viewItem", item);
        }
           
       


        public ActionResult ItemPerPage(int ID = 0)
        {


            ITEM item = db.ITEMs.Find(ID);
            if (item == null)
            {
                return View("PageNotFound");
            }
            return View(item);
        }
           
        



        public string Title { get; set; }
    }
}
