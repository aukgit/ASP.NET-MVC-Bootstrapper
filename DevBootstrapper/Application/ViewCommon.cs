using System.Linq;
using System.Web.Mvc;

namespace DevBootstrapper.Application {
    public static class ViewCommon {
        private static string _productNameMeta;
        
        private static string GetCommonMetadescription() {
            var finalMeta = "";
            if (_productNameMeta == null) {
                var nameList = AppVar.Name.Split(' ').ToList();
                nameList.Add(AppVar.Name);
                nameList.Add(AppVar.Subtitle);
                foreach (var item in nameList) {
                    if (finalMeta.Equals("")) {
                        finalMeta += ",";
                    }
                    finalMeta += item;
                }
                _productNameMeta = finalMeta;
            }
            return _productNameMeta;
        }

        internal static void SetCommonMetaDescriptionToEmpty() {
            _productNameMeta = null;
        }

        public static ActionResult GetGenericError(string title, string message) {
            var dictionary = new ViewDataDictionary {
                {"Title", title},
                {"ErrorMessage", message}
            };
            return new ViewResult {
                ViewName = "_FriendlyError",
                ViewData = dictionary
            };
        }

        public static ActionResult GetAuthenticationError(string title, string message) {
            var dictionary = new ViewDataDictionary {
                {"Title", title},
                {"ErrorMessage", message}
            };
            return new ViewResult {
                ViewName = "_AuthenticationError",
                ViewData = dictionary
            };
        }

        public static void GetTitlePageMeta(dynamic viewBag, string title, string msg = "", string meta = null,
            string keywords = null) {
            viewBag.Title = title;
            viewBag.Message = msg;
            viewBag.Meta = meta + "," + GetCommonMetadescription();
            viewBag.Keywords = keywords + "," + GetCommonMetadescription();
        }

        public static void SetSavedStatus(dynamic viewBag, string msg = null) {
            if (msg == null) {
                msg = "Your previous transaction is successfully saved.";
            }
            viewBag.Success = msg;
        }

        public static void SetErrorStatus(dynamic viewBag, string msg = null) {
            if (msg == null) {
                msg = "Your last transaction is not saved.";
            }
            viewBag.ErrorSave = msg;
        }

    }
}