using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DevBootstrapper.Scheduler;
using FluentScheduler;

namespace DevBootstrapper {
    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true;
            TaskManager.Initialize(registry: new SchedulerRunner());
        }

        public override string GetVaryByCustomString(HttpContext context, string arg) {
            if (arg != null && arg.Equals("byuser", StringComparison.OrdinalIgnoreCase) ||
                arg.Equals("user", StringComparison.OrdinalIgnoreCase)) {
                //HttpCookie cookie = context.Request.Cookies["ASP.NET_SessionID"];
                //if (cookie != null) {
                //    return cookie.Value.ToString();
                    //} else {
                    //    return "const-none";
                //}
                return User.Identity.Name;
            }
            return base.GetVaryByCustomString(context, arg);
        }
    }
}