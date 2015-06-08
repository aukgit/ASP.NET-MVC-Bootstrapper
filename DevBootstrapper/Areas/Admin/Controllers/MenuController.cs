using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.IdentityCustomization;

namespace DevBootstrapper.Areas.Admin.Controllers {
    public class MenuController : Controller {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index() {
            return View(_db.Navigations.Include(n => n.NavigationItems).ToList());
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Navigation navigation) {
            if (ModelState.IsValid) {
                _db.Navigations.Add(navigation);
                _db.SaveChanges();
                ViewBag.Success = "Saved Successfully";
                return View(navigation);
            }
            return View(navigation);
        }

        public ActionResult Edit(Int32 id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var navigation = _db.Navigations.Find(id);
            if (navigation == null) {
                return HttpNotFound();
            }
            return View(navigation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Navigation navigation) {
            if (ModelState.IsValid) {
                _db.Entry(navigation).State = EntityState.Modified;
                _db.SaveChanges();
                ViewBag.Success = "Saved Successfully";
                return RedirectToAction("Index");
            }
            return View(navigation);
        }

        public ActionResult Delete(Int32 id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var navigation = _db.Navigations.Find(id);
            if (navigation == null) {
                return HttpNotFound();
            }
            return View(navigation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            var navigation = _db.Navigations.Find(id);
            _db.Navigations.Remove(navigation);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}