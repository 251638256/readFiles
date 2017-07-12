using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmptyWebApplication {
    public class MyHttpModule : IHttpModule {
        public void Dispose() {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context) {
            //throw new NotImplementedException();
            context.BeginRequest += Context_BeginRequest;
        }

        private void Context_BeginRequest(object sender, EventArgs e) {
            // Request Htt
            Console.WriteLine();
        }
    }
}