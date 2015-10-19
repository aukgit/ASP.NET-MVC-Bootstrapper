#region using block

using System;
using System.Data.Entity;
using System.Web.Mvc;
using DevBootstrapper.Application;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using DevMvcComponent;

#endregion

namespace DevBootstrapper.Areas.Admin.Controllers {
    public class ConfigController : Controller {
        private readonly DevIdentityDbContext _db = new DevIdentityDbContext();

        public ActionResult Index() {
            byte id = 1;

            var coreSetting = _db.CoreSettings.Find(id);
            if (coreSetting == null) {
                return HttpNotFound();
            }
            return View(coreSetting);
        }

        public ActionResult SendEmail(string tab) {
            ViewBag.tab = "#email-setup";
            Mvc.Mailer.QuickSend(AppVar.Setting.DeveloperEmail, "Test Email", "Test Email at " + DateTime.Now);
            try {
                throw new Exception("Testing error mail.");
            } catch (Exception ex) {
                AppVar.Mailer.HandleError(ex, "Test");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CoreSetting coreSetting, string tab) {
            ViewBag.tab = tab;
            if (ModelState.IsValid) {
                _db.Entry(coreSetting).State = EntityState.Modified;
                if (_db.SaveChanges() > -1) {
                    AppConfig.RefreshSetting();
                    ViewCommon.SetSavedStatus(ViewBag);
                    return View(coreSetting);
                }
            }
            ViewCommon.SetErrorStatus(ViewBag);
            return View(coreSetting);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}