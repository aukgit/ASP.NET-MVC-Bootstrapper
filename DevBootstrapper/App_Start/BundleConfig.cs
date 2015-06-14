#region using block

using System.Web.Optimization;

#endregion

namespace DevBootstrapper {
    public class BundleConfig {
        private enum BundleSelector {
            ShowEveryScript,
            EveryScriptThroughBundle,
            EveryScriptGzip
        }
        public static void RegisterBundles(BundleCollection bundles) {

            #region CDN Constants
            const string jQueryCdn = @"//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js";
            //const string jQueryCDN = "http://code.jquery.com/jquery-latest.min.js";
            const string mordernizerCdn = "http://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.min.js";
            //const string respondJsCDN = "http://cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js"
            #endregion

            const BundleSelector bundleSelector = BundleSelector.ShowEveryScript;

            #region jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery", jQueryCdn)
                        .Include("~/Content/Scripts/jquery-2.1.3.min.js") //if no CDN
            );
            #endregion


            switch (bundleSelector) {
                case BundleSelector.ShowEveryScript:
                    #region JavaScripts Bundle

                    #region Validation Bundle & Form Inputs Processing
                    bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                    "~/Content/Scripts/jQuery/jquery.validate.js",
                                    "~/Content/Scripts/jQuery/jquery.validate.unobtrusive.js",
                                    "~/Content/Scripts/jQuery/moment.js",
                                    "~/Content/Scripts/jQuery/bootstrap-datetimepicker.js",
                                    "~/Content/Scripts/jQuery/jquery.elastic.source.js",
                                    "~/Content/Scripts/Bootstrap/bootstrap-select.js",
                                    "~/Content/Scripts/Bootstrap/bootstrap-table.js",
                                    "~/Content/Scripts/Bootstrap/bootstrap-table-filter.js",
                                    "~/Content/Scripts/Bootstrap/bootstrap-table-export.js",
                                    "~/Content/Scripts/Bootstrap/bootstrap3-typeahead.js",
                                    "~/Content/Scripts/Bootstrap/bootstrap-tag.js",
                                    "~/Content/Scripts/devOrg/devOrg.js",
                                    "~/Content/Scripts/devOrg/constants.js",
                                    "~/Content/Scripts/devOrg/selectors.js",
                                    "~/Content/Scripts/devOrg/urls.js",
                                    "~/Content/Scripts/devOrg/dynamic-select.js",
                                    "~/Content/Scripts/devOrg/country-phone.js",
                                    "~/Content/Scripts/devOrg/initialize.js"
                                   ));
                    #endregion


                    #region Upload
                    bundles.Add(new ScriptBundle("~/bundles/upload").Include(
                                        "~/Content/Scripts/Upload/jquery.ui.widget.js",
                                        "~/Content/Scripts/Upload/load-image.all.min.js",
                                        "~/Content/Scripts/Upload/canvas-to-blob.min.js",
                                        "~/Content/Scripts/Upload/jquery.iframe-transport.js",
                                        "~/Content/Scripts/Upload/jquery.fileupload.js",
                                        "~/Content/Scripts/Upload/jquery.fileupload-process.js",
                                        "~/Content/Scripts/Upload/jquery.fileupload-image.js",
                                        "~/Content/Scripts/Upload/jquery.fileupload-audio.js",
                                        "~/Content/Scripts/Upload/jquery.fileupload-video.js",
                                        "~/Content/Scripts/Upload/jquery.fileupload-validate.js",
                                        "~/Content/Scripts/devOrg/upload.js"
                                   ));
                    #endregion

                    #region Bootstrap
                    bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                  "~/Content/Scripts/devOrg/byId.js",
                                  "~/Content/Scripts/Bootstrap/bootstrap.js", // 3.1.2
                                  "~/Content/Scripts/Bootstrap/star-rating.js",
                                  "~/Content/Scripts/jQuery/underscore.js"
                                  ));
                    #endregion

                    #endregion

                    #region CSS Bundles

                    bundles.Add(new StyleBundle("~/Content/css/styles").Include(
                                        "~/Content/css/bootstrap.css",
                                        "~/Content/css/less-imports.css",
                                        "~/Content/css/animate.css",
                                        "~/Content/css/font-awesome.css",
                                        "~/Content/css/animate-refresh.css",
                                        "~/Content/css/site.css",
                                        "~/Content/css/flags32.css",
                                        "~/Content/css/flags32-combo.css",
                                        "~/Content/css/Upload/jquery.fileupload.css",

                                        "~/Content/css/bootstrap-datetimepicker.css",
                                        "~/Content/css/bootstrap-table.css",
                                        "~/Content/css/bootstrap-select.css",

                                        "~/Content/css/color-fonts.css",
                                        "~/Content/css/star-rating.css",
                                        "~/Content/css/override-mvc.css",

                                        "~/Content/css/base-theme-styles.css",
                                        "~/Content/css/style.css",

                                        "~/Content/css/responsive.css",
                                        "~/Content/revolution-slider/css/settings.css",

                                        "~/Content/css/owl.carousel.css",
                                        "~/Content/css/owl.theme.css",
                                        "~/Content/css/owl.custom.finalize.css",

                                        "~/Content/css/seo-optimize.css",
                                        "~/Content/css/front-developer.css",
                                        "~/Content/css/footer-fixing.css",
                                        "~/Content/css/utilities.css"

                    ));

                    #endregion

                    break;
                case BundleSelector.EveryScriptThroughBundle:
                    #region Java Scripts Bundle

                    #region Validation Bundle & Form Inputs Processing
                    bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                    "~/Content/Published.Scripts/jqueryval.min.js"
                                   ));
                    #endregion


                    #region Upload
                    bundles.Add(new ScriptBundle("~/bundles/upload").Include(
                                   "~/Content/Published.Scripts/upload.min.js"
                                   ));
                    #endregion

                    #region Bootstrap
                    bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                  "~/Content/Published.Scripts/front-end.min.js"
                                  ));
                    #endregion

                    #endregion

                    #region CSS Bundles

                    bundles.Add(new StyleBundle("~/Content/css/styles").Include(
                                  "~/Content/Published.Styles/Styles.min.css"

                    ));

                    #endregion
                    break;
                case BundleSelector.EveryScriptGzip:
                    #region Java Scripts Bundle

                    #region Validation Bundle & Form Inputs Processing
                    bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                    "~/Content/Scripts/Bundles/jqueryval.min.js.gz"
                                   ));
                    #endregion


                    #region Upload
                    bundles.Add(new ScriptBundle("~/bundles/upload").Include(
                                   "~/Content/Scripts/Bundles/UploadJs.min.js.gz"
                                   ));
                    #endregion

                    #region Bootstrap
                    bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                  "~/Content/Scripts/Bundles/FrontEndJs.min.js.gz"
                                  ));
                    #endregion

                    #endregion

                    #region CSS Bundles

                    bundles.Add(new StyleBundle("~/Content/css/styles").Include(
                                  "~/Content/CompactCSS.min.css.gz"

                    ));

                    #endregion
                    break;

            }




            #region Configs

            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = false;

            #endregion

        }
    }
}