using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Cache.Controllers
{
    public class HomeController : Controller
    {
        private string key = "thisismydata";
        private string newKey = "newdata";
        private static Dictionary<string, object> cacheDict = new Dictionary<string, object>(); // 实例对象每次都不一样?? 没次访问都是不同的controller实例?? 
        private object obj = new object();

        // GET: Home
        public ActionResult Index()
        {
            Thread.Sleep(5000);
            CacheDependency dependency;
            DateTime absoluteExpiration;
            TimeSpan slidingExpiration;

            //HttpRuntime.Cache.Insert(key, "我才是真的缓存数据(十分之一秒)", null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromTicks(10000000),CacheItemPriority.Normal, (string key, object value, CacheItemRemovedReason reason) => {
            //    Console.WriteLine();
            //});

            //TimeSpan span = System.Web.Caching.Cache.NoSlidingExpiration;
            //HttpRuntime.Cache.Add(newKey, "我才是真的缓存数据(一秒)", null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High,
            //    (string key, object value, CacheItemRemovedReason reason) => {
            //    // 访问过期后就不会删除 但不会立即调用这个方法(有可能在过了很久后才会掉) 只有主动访问了缓存才会立即调用 

            //    Console.WriteLine();
            //});

            lock(obj) {
                if (!cacheDict.ContainsKey(newKey))
                    cacheDict.Add(newKey, "字典测试数据");
            }
            
            return View();
        }


        public ActionResult About() {

            //string cdata = HttpRuntime.Cache.Get(key)?.ToString();
            //string newData = HttpRuntime.Cache.Get(newKey)?.ToString();
            //HttpRuntime.Cache.Remove(newKey);
            //ViewData["data"] = cdata;
            //ViewData["newData"] = newData;

            string v = string.Empty;
            lock (obj) {
                if (cacheDict.ContainsKey(newKey)) {
                    v = cacheDict[newKey]?.ToString();
                    cacheDict.Remove(newKey);
                }
            }
            ViewData["data"] = v;

            return View();
        }
    }
}