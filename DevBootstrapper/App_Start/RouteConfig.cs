using System.Web.Mvc;
using System.Web.Routing;

namespace DevBootstrapper {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah");

            #region Login, Register, Authentication Additional Routes

            routes.MapRoute("RegisterConfig", "Register",
                new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                new[] { "DevBootstrapper.Controllers" }
                );
            routes.MapRoute("SignInConfig", "SignIn",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                new[] { "DevBootstrapper.Controllers" }
                );
            routes.MapRoute("LoginConfig", "Login",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                new[] { "DevBootstrapper.Controllers" }
                );

            routes.MapRoute("SignOut", "SignOut",
                new { controller = "Account", action = "SignOut", id = UrlParameter.Optional },
                new[] { "DevBootstrapper.Controllers" }
                );

            routes.MapRoute("ExternalSigninConfig", "ExtSignin",
                new { controller = "Account", action = "ExternalLogin", id = UrlParameter.Optional },
                new[] { "DevBootstrapper.Controllers" }
                );

            #endregion

            #region Default Route

            routes.MapRoute("Direct", "{action}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "DevBootstrapper.Controllers" }
                );
            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "DevBootstrapper.Controllers" }
                );

            #endregion
        }
    }
}