using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace 一般处理 {
    /// <summary>
    /// MyFirst 的摘要说明
    /// </summary>
    public class MyFirst : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World, First MyFirst.ashx");

            //HttpRuntime.Cache.Add("Key", "Object",null,DateTime.Now.AddYears(20),TimeSpan.FromDays(1),null,(string key, object value, CacheItemRemovedReason reason) => { });

        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}