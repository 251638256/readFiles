using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace SourceCode {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ExceptionFilter());
        }
    }

    public class ExceptionFilter : HandleErrorAttribute {
        private ILogger log = LogManager.GetCurrentClassLogger();
        public override void OnException(ExceptionContext filterContext) {
            log.Error(filterContext.Exception);

            string jsonString = JsonConvert.SerializeObject(new { status = 0, msg = filterContext.Exception.Message });
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()) {
                filterContext.HttpContext.Response.Write(jsonString);
            }

            filterContext.HttpContext.Response.Redirect("/error/Index");
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }
    }
}
