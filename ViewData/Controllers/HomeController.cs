using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViewData.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            TempData["Key"] = "value";
            //return RedirectToAction("About");
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";
            //string v = TempData["Key"]?.ToString();
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}