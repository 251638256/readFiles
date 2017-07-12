using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFom {
    /// <summary>
    /// Handler1ashx 的摘要说明
    /// </summary>
    public class Handler1ashx : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");


        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }

    public class MyHttpModule : IHttpModule {
        public void Dispose() {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context) {
            //throw new NotImplementedException();
            context.BeginRequest += Context_BeginRequest;
        }

        private void Context_BeginRequest(object sender, EventArgs e) {
            // request
        }
    }
}