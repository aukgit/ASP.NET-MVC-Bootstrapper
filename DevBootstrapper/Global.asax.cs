using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DevBootstrapper {
    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true;
        }

        public override string GetVaryByCustomString(HttpContext context, string arg) {
            if (arg != null && arg.Equals("byuser", StringComparison.OrdinalIgnoreCase) ||
                arg.Equals("user", StringComparison.OrdinalIgnoreCase)) {
                var cookie = context.Request.Cookies["ASP.NET_SessionID"];
                if (cookie != null) {
                    return cookie.Value;
                    //} else {
                    //    return "const-none";
                }
            }
            return base.GetVaryByCustomString(context, arg);
        }
    }
}