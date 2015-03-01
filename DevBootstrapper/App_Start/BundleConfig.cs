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
                .Include("~/Content/Scripts/jQuery/jquery-2.1.3.min.js") //if no CDN
                );

            #endregion

            #region Validation Bundle & Form Inputs Processing

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Content/Scripts/jQuery/jquery.validate*",
                "~/Content/Scripts/underscore.js",
                "~/Content/Scripts/jQuery/moment.js",
                "~/Content/Scripts/Bootstrap/bootstrap-datetimepicker.js",
                "~/Content/Scripts/Bootstrap/bootstrap-select.js",
                "~/Content/Scripts/Bootstrap/bootstrap-table.js",
                "~/Content/Scripts/Bootstrap/bootstrap-table-filter.js",
                "~/Content/Scripts/Bootstrap/bootstrap-table-export.js",
                "~/Content/Scripts/Bootstrap/star-rating.js",
                "~/Content/Scripts/DevOrgCompoents/DevOrgComponent.js",
                "~/Content/Scripts/DevOrgCompoents/CustomJS.js"
                ));

            #endregion

            #region Mordernizer

            bundles.Add(new ScriptBundle("~/bundles/modernizr", mordernizerCDN)
                .Include("~/Content/Scripts/modernizr-*") //if no CDN
                );

            #endregion

            #region Bootstrap

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Content/Scripts/Bootstrap/bootstrap.js", // 3.1.2
                "~/Content/Scripts/Bootstrap/respond.js",
                "~/Content/Scripts/Bootstrap/common-tasks-run-every-page.js"
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