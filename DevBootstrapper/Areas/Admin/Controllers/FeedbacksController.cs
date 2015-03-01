﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DevBootstrapper.Controllers;
using DevBootstrapper.Models.POCO.IdentityCustomization;

namespace DevBootstrapper.Areas.Admin.Controllers {
    public class FeedbacksController : BasicController {
        // Generated by Alim Ul Karim on behalf of Developers Organism.
        // Find us developers-organism.com
        // https://www.facebook.com/DevelopersOrganism
        // mailto:info@developers-organism.com

        public FeedbacksController()
            : base(true) {
        }

        public void GetDropDowns() {
        }

        public ActionResult Index() {
            return View("Index", Db.Feedbacks.Where(n => !n.IsViewed).ToList());
        }

        public ActionResult UnSolved() {
            return View("Index", Db.Feedbacks.Where(n => n.IsUnSolved).ToList());
        }

        public ActionResult IsInProcess() {
            return View("Index", Db.Feedbacks.Where(n => n.IsSolved).ToList());
        }

        public ActionResult Solved() {
            return View("Index", Db.Feedbacks.Where(n => n.IsSolved).ToList());
        }

        public ActionResult Details(Int64 id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var feedback = Db.Feedbacks.Find(id);
            if (feedback == null) {
                return HttpNotFound();
            }
            return View(feedback);
        }

        public ActionResult Edit(Int64 id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var feedback = Db.Feedbacks.Find(id);
            if (feedback == null) {
                return HttpNotFound();
            }
            GetDropDowns();
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Feedback feedback) {
            GetDropDowns();
            if (ModelState.IsValid) {
                Db.Entry(feedback).State = EntityState.Modified;
                feedback.IsSolved = true;
                feedback.IsUnSolved = false;
                feedback.IsViewed = true;
                feedback.IsInProcess = false;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            AppVar.SetErrorStatus(ViewBag);
            return View(feedback);
        }

        public ActionResult Delete(long id) {
            var feedback = Db.Feedbacks.Find(id);
            Db.Feedbacks.Remove(feedback);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}