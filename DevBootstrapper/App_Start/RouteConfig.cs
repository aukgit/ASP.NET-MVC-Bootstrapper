using System.Web.Mvc;
using System.Web.Routing;

namespace DevBootstrapper {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah");

            const string parentControllerNameSpace = "DevBootstrapper.Controllers";

            #region Login, Register, Authentication Additional Routes

            const string accountController = "Account";

            routes.MapRoute(
                name: "RegisterConfig",
                url: "Register",
                defaults: new {
                    controller = accountController,
                    action = "Register",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { parentControllerNameSpace }
            );
            routes.MapRoute(
                name: "SignInConfig",
                url: "SignIn",
                defaults: new {
                    controller = accountController,
                    action = "Login",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { parentControllerNameSpace }
            );

            routes.MapRoute(
                name: "LoginConfig",
                url: "Login",
                defaults: new {
                    controller = accountController,
                    action = "Login",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { parentControllerNameSpace }
            );

            routes.MapRoute(
                name: "SignOut",
                url: "SignOut",
                defaults: new {
                    controller = accountController,
                    action = "SignOut",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { parentControllerNameSpace }
            );

            routes.MapRoute(
                name: "ExternalSigninConfig",
                url: "ExtSignin",
                defaults: new {
                    controller = accountController,
                    action = "ExternalLogin",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { parentControllerNameSpace }
            );

            #endregion

            #region Default Route

            routes.MapRoute(
                name: "Direct",
                url: "{action}",
                defaults: new {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { parentControllerNameSpace }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { parentControllerNameSpace }
            );



            #endregion
        }
    }
}