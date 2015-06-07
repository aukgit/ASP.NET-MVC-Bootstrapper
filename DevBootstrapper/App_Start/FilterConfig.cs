#region using block

using System.Web.Mvc;
using DevBootstrapper.Filter;

#endregion

namespace DevBootstrapper {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AreaAuthorizeAttribute());
        }
    }
}