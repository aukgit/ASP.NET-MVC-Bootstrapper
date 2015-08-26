#region using block

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace DevBootstrapper {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah");

            #region variables
            const string parentControllerNamespace = "DevBootstrapper.Controllers";
            const string accountController = "Account";
            #endregion

            #region Login, Register, Authentication Additional Routes

            routes.MapRoute(
                name: "RegisterConfig",
                url: "Register",
                defaults: new { controller = accountController, action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { parentControllerNamespace }
            );
            routes.MapRoute(
                name: "SignInConfig",
                url: "SignIn",
                defaults: new { controller = accountController, action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { parentControllerNamespace }
            );
            routes.MapRoute(
                name: "LoginConfig",
                url: "Login",
                defaults: new { controller = accountController, action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { parentControllerNamespace }
            );

            routes.MapRoute(
                name: "SignOut",
                url: "SignOut",
                defaults: new { controller = accountController, action = "SignOut" },
                namespaces: new string[] { parentControllerNamespace }
            );

            routes.MapRoute(
                name: "LogOff",
                url: "LogOff",
                defaults: new { controller = accountController, action = "SignOut" },
                namespaces: new string[] { parentControllerNamespace }
            );

            routes.MapRoute(
               name: "ExternalSigninConfig",
               url: "ExtSignin",
               defaults: new { controller = accountController, action = "ExternalLogin", id = UrlParameter.Optional },
               namespaces: new string[] { parentControllerNamespace }
            );
            #endregion

            #region Default Route : By default indexes
            //routes.MapRoute(
            //    name: "Direct",
            //    url: "{action}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new string[] { parentControllerNamespace }
            //);
         
            routes.MapRoute(
                name: "ContactUs",
                url: "ContactUs",
                defaults: new { controller = "Home", action = "ContactUs", id = UrlParameter.Optional },
                namespaces: new string[] { parentControllerNamespace }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { parentControllerNamespace }
            );
            #endregion
        }
    }
}