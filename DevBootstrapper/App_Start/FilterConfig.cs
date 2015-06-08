using System.Web.Mvc;
using DevBootstrapper.Filter;

namespace DevBootstrapper {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AreaAuthorizeAttribute());
        }
    }
}