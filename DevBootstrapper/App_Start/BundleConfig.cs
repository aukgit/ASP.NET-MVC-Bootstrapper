using System.Web.Optimization;

namespace DevBootstrapper {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            #region CDN Constants

            //const string jQueryCDN = "http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js";
            const string jQueryCdn = "http://code.jquery.com/jquery-latest.min.js";
            const string mordernizerCdn = "http://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.min.js";
            //const string respondJsCDN = "http://cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js"

            #endregion

            #region Java Scripts Bundle

            #region jQuery

            bundles.Add(new ScriptBundle("~/bundles/jquery", jQueryCdn)
                .Include("~/Content/Scripts/jQuery/jquery-2.1.3.min.js") //if no CDN
                );

            #endregion

            #region Validation Bundle & Form Inputs Processing

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Content/Scripts/jQuery/jquery.validate*",
                "~/Content/Scripts/jQuery/underscore.js",
                "~/Content/Scripts/jQuery/moment.js",
                "~/Content/Scripts/jQuery/jquery.elastic.source.js",
                "~/Content/Scripts/Bootstrap/bootstrap-datetimepicker.js",
                "~/Content/Scripts/Bootstrap/bootstrap-select.js",
                "~/Content/Scripts/Bootstrap/bootstrap-table.js",
                "~/Content/Scripts/Bootstrap/bootstrap-table-filter.js",
                "~/Content/Scripts/Bootstrap/bootstrap-table-export.js",
                "~/Content/Scripts/Bootstrap/star-rating.js",
                "~/Content/Scripts/Bootstrap/bootstrap3-typeahead.js",
                "~/Content/Scripts/Bootstrap/bootstrap-tag.js",
                "~/Content/Scripts/DevOrgCompoents/DevOrgComponent.js",
                "~/Content/Scripts/DevOrgCompoents/DevOrgDynamicSelect.js",
                "~/Content/Scripts/DevOrgCompoents/Every-Component-Runner.js"
                ));

            #endregion

            #region Upload
            bundles.Add(new ScriptBundle("~/bundles/upload").Include(
                                "~/Content/Scripts/jQuery/Upload/jquery.ui.widget.js",
                                "~/Content/Scripts/jQuery/Upload/load-image.all.min.js",
                                "~/Content/Scripts/jQuery/Upload/canvas-to-blob.min.js",
                                "~/Content/Scripts/jQuery/Upload/jquery.iframe-transport.js",
                                "~/Content/Scripts/jQuery/Upload/jquery.fileupload.js",
                                "~/Content/Scripts/jQuery/Upload/jquery.fileupload-process.js",
                                "~/Content/Scripts/jQuery/Upload/jquery.fileupload-image.js",
                                "~/Content/Scripts/jQuery/Upload/jquery.fileupload-audio.js",
                                "~/Content/Scripts/jQuery/Upload/jquery.fileupload-video.js",
                                "~/Content/Scripts/jQuery/Upload/jquery.fileupload-validate.js",                
                                "~/Content/Scripts/DevOrgCompoents/Upload/devOrgUploadConfig.js"
                           ));
            #endregion

            #region Mordernizer

            bundles.Add(new ScriptBundle("~/bundles/modernizr", mordernizerCdn)
                .Include("~/Content/Scripts/Bootstrap/modernizr-*") //if no CDN
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
                "~/Content/css/size.css",
                "~/Content/css/flags32.css",
                "~/Content/css/flags32-combo.css",
                "~/Content/css/bootstrap-datetimepicker.css",
                "~/Content/css/bootstrap-table.css",
                "~/Content/css/color-fonts.css",
                "~/Content/css/font-awesome.css",
                "~/Content/css/star-rating.css",
                "~/Content/css/miscellaneous.css",
                "~/Content/css/bootstrap-select.css",
                "~/Content/css/bootstrap-tag.css",
                "~/Content/css/fixing-css.css",
                "~/Content/css/core-developer.css",
                "~/Content/css/core-developer-additions.css",
                "~/Content/css/front-developer.css"
                ));

            #endregion

            #region Configs

            bundles.UseCdn = false;
            BundleTable.EnableOptimizations = false;

            #endregion
        }
    }
}