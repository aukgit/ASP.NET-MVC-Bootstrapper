using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.IdentityCustomization;

namespace DevBootstrapper.Areas.Admin.Controllers {
    public class NavItemsController : Controller {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        private List<NavigationItem> GetItems(int? navitionId = null) {
            if (navitionId == null) {
                return _db.NavigationItems.ToList();
            }
            return _db.NavigationItems.Where(n => n.NavigationID == navitionId).ToList();
        }

        private void AddMenuName(int id) {
            var nav = _db.Navigations.Find(id);
            ViewBag.MenuName = nav.Name;
            ViewBag.NavigationID = id;
        }

        public ActionResult List(int id) {
            AddMenuName(id);
            return View(GetItems(id));
        }

        private void HasDropDownAttr(NavigationItem navigationItem) {
            if (!navigationItem.HasDropDown) {
                navigationItem.ParentNavigationID = null;
            }
        }

        public ActionResult Add(int id) {
            AddMenuName(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NavigationItem navigationItem) {
            AddMenuName(navigationItem.NavigationID);
            if (ModelState.IsValid) {
                HasDropDownAttr(navigationItem);
                _db.NavigationItems.Add(navigationItem);
                AppVar.SetSavedStatus(ViewBag);
                _db.SaveChanges();
                AppConfig.Caches.RemoveAllFromCache();
                return View(navigationItem);
            }
            AppVar.SetErrorStatus(ViewBag);
            return View(navigationItem);
        }

        public ActionResult Edit(Int32 id, int navigationId) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Editing = true;
            var navigationItem = _db.NavigationItems.Find(id);
            if (navigationItem == null) {
                return HttpNotFound();
            }
            AddMenuName(navigationId);
            return View(navigationItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NavigationItem navigationItem) {
            ViewBag.Editing = true;
            HasDropDownAttr(navigationItem);
            if (ModelState.IsValid) {
                _db.Entry(navigationItem).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("List", new { id = navigationItem.NavigationID });
            }
            AddMenuName(navigationItem.NavigationID);
            AppConfig.Caches.RemoveAllFromCache();
            return View(navigationItem);
        }

        public ActionResult Delete(int id, int navigationId) {
            var navigationItem = _db.NavigationItems.Find(id);
            _db.NavigationItems.Remove(navigationItem);
            _db.SaveChanges();
            AddMenuName(navigationId);
            AppConfig.Caches.RemoveAllFromCache();
            return RedirectToAction("List", new { id = navigationId });
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}