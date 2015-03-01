using System.Web.Optimization;

namespace DevBootstrapper {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            #region CDN Constants

            //const string jQueryCDN = "http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js";
            const string jQueryCDN = "http://code.jquery.com/jquery-latest.min.js";
            const string mordernizerCDN = "http://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.min.js";
            //const string respondJsCDN = "http://cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js"

            #endregion

            #region Java Scripts Bundle

            #region jQuery

            bundles.Add(new ScriptBundle("~/bundles/jquery", jQueryCDN)
                .Include("~/Scripts/jquery-2.1.1.min.js") //if no CDN
                );

            #endregion

            #region Validation Bundle & Form Inputs Processing

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Content/Scripts/jquery.validate*",
                "~/Scripts/underscore.js",
                "~/Scripts/moment.js",
                "~/Scripts/bootstrap-datetimepicker.js",
                "~/Scripts/bootstrap-select.js",
                "~/Content/Scripts/bootstrap-table.js",
                "~/Content/Scripts/bootstrap-table-filter.js",
                "~/Content/Scripts/bootstrap-table-export.js",
                "~/Content/Scripts/star-rating.js",
                "~/Scripts/CustomScripts/DevOrgComponent.js",
                "~/Scripts/CustomScripts/CustomJS.js"
                ));

            #endregion

            #region Mordernizer

            bundles.Add(new ScriptBundle("~/bundles/modernizr", mordernizerCDN)
                .Include("~/Content/Scripts/modernizr-*") //if no CDN
                );

            #endregion

            #region Bootstrap

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Content/Scripts/bootstrap.js", // 3.1.2
                "~/Content/Scripts/respond.js",
                "~/Scripts/CustomScripts/CommonJsEveryPage.js"
                ));

            #endregion

            #endregion

            #region CSS Bundles

            bundles.Add(new StyleBundle("~/Content/css/styles").Include(
                "~/Content/css/bootstrap.theme.unitied.css",
                "~/Content/css/site.css",
                "~/Content/css/flags32.css",
                "~/Content/css/flags32-combo.css",
                "~/Content/css/bootstrap-datetimepicker.css",
                "~/Content/css/bootstrap-table.css",
                "~/Content/css/color-fonts.css",
                "~/Content/css/font-awesome.css",
                "~/Content/css/star-rating.css",
                "~/Content/css/miscellaneous.css",
                "~/Content/css/bootstrap-select.css",
                "~/Content/css/overridecss.css"
                ));

            #endregion

            #region Configs

            bundles.UseCdn = false;
            BundleTable.EnableOptimizations = false;

            #endregion
        }
    }
}