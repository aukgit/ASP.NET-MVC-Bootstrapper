﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevBootstrapper.Models.POCO.IdentityCustomization;

namespace DevBootstrapper.Controllers {
    public class CountriesController : AdvanceController {
        #region Constructors

        public CountriesController()
            : base(true) {
            ViewBag.controller = ControllerName;
        }

        #endregion

        #region View tapping

        /// <summary>
        ///     Always tap once before going into the view.
        /// </summary>
        /// <param name="ViewStates">Say the view state, where it is calling from.</param>
        /// <param name="Country">Gives the model if it is a editing state or creating posting state or when deleting.</param>
        /// <returns>If successfully saved returns true or else false.</returns>
        private bool ViewTapping(ViewStates view, Country country = null) {
            switch (view) {
                case ViewStates.Index:
                    break;
                case ViewStates.Create:
                    break;
                case ViewStates.CreatePost: // before saving it
                    break;
                case ViewStates.Edit:
                    break;
                case ViewStates.Details:
                    break;
                case ViewStates.EditPost: // before saving it
                    break;
                case ViewStates.Delete:
                    break;
            }
            return true;
        }

        #endregion

        #region Save database common method

        /// <summary>
        ///     Better approach to save things into database(than db.SaveChanges()) for this controller.
        /// </summary>
        /// <param name="ViewStates">Say the view state, where it is calling from.</param>
        /// <param name="Country">Your model information to send in email to developer when failed to save.</param>
        /// <returns>If successfully saved returns true or else false.</returns>
        private bool SaveDatabase(ViewStates view, Country country = null) {
            // working those at HttpPost time.
            switch (view) {
                case ViewStates.Create:
                    break;
                case ViewStates.Edit:
                    break;
                case ViewStates.Delete:
                    break;
            }

            try {
                var changes = db.SaveChanges(country);
                if (changes > 0) {
                    return true;
                }
            } catch (Exception ex) {
                throw new Exception("Message : " + ex.Message + " Inner Message : " + ex.InnerException.Message);
            }
            return false;
        }

        #endregion

        #region Index

        public ActionResult Index() {
            var viewOf = ViewTapping(ViewStates.Index);
            return View(db.Countries.ToList());
        }

        #endregion

        #region Details

        public ActionResult Details(Int32 id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = db.Countries.Find(id);
            if (country == null) {
                return HttpNotFound();
            }
            var viewOf = ViewTapping(ViewStates.Details, country);
            return View(country);
        }

        #endregion

        #region Removing output cache

        public void RemoveOutputCache(string url) {
            HttpResponse.RemoveOutputCacheItem(url);
        }

        #endregion

        #region Enums

        internal enum ViewStates {
            Index,
            Create,
            CreatePost,
            Edit,
            EditPost,
            Details,
            Delete,
            DeletePost
        }

        #endregion

        #region Developer Comments - Alim Ul karim

        // Generated by Alim Ul Karim on behalf of Developers Organism.
        // Find us developers-organism.com
        // https://www.facebook.com/DevelopersOrganism
        // mailto:alim@developers-organism.com		

        #endregion

        #region Constants

        private const string DeletedError =
            "Sorry for the inconvenience, last record is not removed. Please be in touch with admin.";

        private const string DeletedSaved = "Removed successfully.";
        private const string EditedSaved = "Modified successfully.";

        private const string EditedError =
            "Sorry for the inconvenience, transaction is failed to save into the database. Please be in touch with admin.";

        private const string CreatedError = "Sorry for the inconvenience, couldn't create the last transaction record.";
        private const string CreatedSaved = "Transaction is successfully added to the database.";
        private const string ControllerName = "Countries";

        /// Constant value for where the controller is actually visible.
        private const string ControllerVisibleUrl = "";

        #endregion

        #region DropDowns Generate

        public void GetDropDowns(Country country = null) {
        }

        public void GetDropDowns(Int32 id) {
        }

        #endregion

        #region Index Find - Commented

        /*
        public ActionResult Index(System.Int32 id) {
			bool viewOf = ViewTapping(ViewStates.Index);
            return View(db.Countries.Where(n=> n. == id).ToList());
        }
		*/

        #endregion

        #region Create or Add

        public ActionResult Create() {
            GetDropDowns();
            var viewOf = ViewTapping(ViewStates.Create);
            return View();
        }

        /*
		public ActionResult Create(System.Int32 id) {        
			GetDropDowns(id); // Generate hidden.
			bool viewOf = ViewTapping(ViewStates.Create);
            return View();
        }
		*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country country) {
            var viewOf = ViewTapping(ViewStates.CreatePost, country);
            GetDropDowns(country);
            if (ModelState.IsValid) {
                db.Countries.Add(country);
                var state = SaveDatabase(ViewStates.Create, country);
                if (state) {
                    AppVar.SetSavedStatus(ViewBag, CreatedSaved); // Saved Successfully.
                } else {
                    AppVar.SetErrorStatus(ViewBag, CreatedError); // Failed to save
                }

                return View(country);
            }
            AppVar.SetErrorStatus(ViewBag, CreatedError); // Failed to Save
            return View(country);
        }

        #endregion

        #region Edit or modify record

        public ActionResult Edit(Int32 id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = db.Countries.Find(id);
            if (country == null) {
                return HttpNotFound();
            }
            var viewOf = ViewTapping(ViewStates.Edit, country);
            GetDropDowns(country); // Generating drop downs
            return View(country);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country country) {
            var viewOf = ViewTapping(ViewStates.EditPost, country);
            if (ModelState.IsValid) {
                db.Entry(country).State = EntityState.Modified;
                var state = SaveDatabase(ViewStates.Edit, country);
                if (state) {
                    AppVar.SetSavedStatus(ViewBag, EditedSaved); // Saved Successfully.
                } else {
                    AppVar.SetErrorStatus(ViewBag, EditedError); // Failed to Save
                }

                return RedirectToAction("Index");
            }

            GetDropDowns(country);
            AppVar.SetErrorStatus(ViewBag, EditedError); // Failed to save
            return View(country);
        }

        #endregion

        #region Delete or remove record

        public ActionResult Delete(int id) {
            var country = db.Countries.Find(id);
            var viewOf = ViewTapping(ViewStates.Delete, country);
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            var country = db.Countries.Find(id);
            var viewOf = ViewTapping(ViewStates.DeletePost, country);
            db.Countries.Remove(country);
            var state = SaveDatabase(ViewStates.Delete, country);
            if (!state) {
                AppVar.SetErrorStatus(ViewBag, DeletedError); // Failed to Save
                return View(country);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}