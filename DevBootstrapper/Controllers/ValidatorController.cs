#region using block

using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using DevBootstrapper.Application;
using DevBootstrapper.Constants;
using DevBootstrapper.Modules.DevUser;

#endregion

namespace DevBootstrapper.Controllers {
    public class ValidatorController : Controller {
        [HttpPost]
        [OutputCache(CacheProfile = "Long", VaryByParam = "id", VaryByCustom = "byuser")]
        [ValidateAntiForgeryToken]
        public ActionResult GetUsername(string id, string requestVerificationToken) {
            var returnParam = true;
            var max = 30;
            var min = 3;
            try {
                if (id == null || id.Length < 3) {
                    return Json(!returnParam, JsonRequestBehavior.AllowGet);
                }
                if (SessionNames.IsValidationExceed("username")) {
                    throw new Exception("Exceed the limit of try");
                }
                var userPattern = "^([A-Za-z]|[A-Za-z0-9_.]+)$";
                if (Regex.IsMatch(id, userPattern, RegexOptions.Compiled) && (id.Length >= min && id.Length <= max)) {
                    if (UserManager.IsUserNameExist(id)) {
                        return Json(returnParam, JsonRequestBehavior.AllowGet);
                    }
                    return Json(!returnParam, JsonRequestBehavior.AllowGet); // only true
                }
            } catch (Exception ex) {
                AppVar.Mailer.HandleError(ex, "Validate Username");
            }
            //found e false
            return Json(!returnParam, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [OutputCache(CacheProfile = "Long", VaryByParam = "id", VaryByCustom = "byuser")]
        [ValidateAntiForgeryToken]
        public ActionResult Username(string id, string requestVerificationToken) {
            var returnParam = true;
            var max = 30;
            var min = 3;
            try {
                if (id == null || id.Length < 3) {
                    return Json(!returnParam, JsonRequestBehavior.AllowGet);
                }
                if (SessionNames.IsValidationExceed("username")) {
                    throw new Exception("Exceed the limit of try");
                }
                var userPattern = "^([A-Za-z]|[A-Za-z0-9_.]+)$";
                if (Regex.IsMatch(id, userPattern, RegexOptions.Compiled) && (id.Length >= min && id.Length <= max)) {
                    if (UserManager.IsUserNameExist(id)) {
                        return Json(!returnParam, JsonRequestBehavior.AllowGet);
                    }
                    return Json(returnParam, JsonRequestBehavior.AllowGet); // only true
                }
            } catch (Exception ex) {
                AppVar.Mailer.HandleError(ex, "Validate Username");
            }
            //found e false
            return Json(!returnParam, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [OutputCache(CacheProfile = "Long", VaryByParam = "id", VaryByCustom = "byuser")]
        [ValidateAntiForgeryToken]
        public ActionResult Email(string id, string requestVerificationToken) {
            if (SessionNames.IsValidationExceed("Email")) {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            try {
                if (id == null || id.Length < 5)
                    return Json(false, JsonRequestBehavior.AllowGet);

                var email = id;

                var emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
                if (Regex.IsMatch(email, emailPattern)) {
                    if (!UserManager.IsEmailExist(email)) {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            } catch (Exception ex) {
                AppVar.Mailer.HandleError(ex, "Validate Email");

                return Json(false);
            }
        }
    }
}