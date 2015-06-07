#region using block

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace DevBootstrapper {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah");

            const string parentControllerNameSpace = "DevBootstrapper.Controllers";

            #region Login, Register, Authentication Additional Routes

            const string accountController = "Account";

            routes.MapRoute("RegisterConfig", "Register", new {
                controller = accountController,
                action = "Register",
                id = UrlParameter.Optional
            }, new[] {parentControllerNameSpace}
                );
            routes.MapRoute("SignInConfig", "SignIn", new {
                controller = accountController,
                action = "Login",
                id = UrlParameter.Optional
            }, new[] {parentControllerNameSpace}
                );

            routes.MapRoute("LoginConfig", "Login", new {
                controller = accountController,
                action = "Login",
                id = UrlParameter.Optional
            }, new[] {parentControllerNameSpace}
                );

            routes.MapRoute("SignOut", "SignOut", new {
                controller = accountController,
                action = "SignOut",
                id = UrlParameter.Optional
            }, new[] {parentControllerNameSpace}
                );

            routes.MapRoute("ExternalSigninConfig", "ExtSignin", new {
                controller = accountController,
                action = "ExternalLogin",
                id = UrlParameter.Optional
            }, new[] {parentControllerNameSpace}
                );

            #endregion

            #region Default Route

            routes.MapRoute("Direct", "{action}", new {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            }, new[] {parentControllerNameSpace}
                );
            routes.MapRoute("Default", "{controller}/{action}/{id}", new {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            }, new[] {parentControllerNameSpace}
                );

            #endregion
        }
    }
}