using System;
using System.Collections;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SourceCode.Controllers {
    public class HomeController : Controller {

        string key = "mydata";
        public ActionResult Index() {

            var container = Hashtable.Synchronized(new Hashtable());
            

            Thread.Sleep(10000);
            TimeSpan span = new TimeSpan(0);
            HttpRuntime.Cache.Insert(key, "我才是真的缓存数据", null, System.Web.Caching.Cache.NoAbsoluteExpiration, span);
            return View();
        }

        public ActionResult About() {
            var data = HttpRuntime.Cache.Get(key)?.ToString();
            ViewData["data"] = data;
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}