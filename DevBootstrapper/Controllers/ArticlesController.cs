﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevBootstrapper.Controllers;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.EntityModel.POCO;
using DevTrends.MvcDonutCaching;

namespace DevBootstrapper.Controllers
{
    public class ArticlesController : GenericController<MagazineEntities> {

		#region Developer Comments - Alim Ul karim
        /*
         *  Generated by Alim Ul Karim on behalf of Developers Organism.
         *  Find us developers-organism.com
         *  https://fb.com/DevelopersOrganism
         *  mailto:alim@developers-organism.com	
         *  Google 'https://www.google.com.bd/search?q=Alim-ul-karim'
         *  First Written : 23 Mar 2014
         *  Modified      : 03 March 2015
         * * */
		#endregion

		#region Constants

		const string DeletedError = "Sorry for the inconvenience, last record is not removed. Please be in touch with admin.";
		const string DeletedSaved = "Removed successfully.";
		const string EditedSaved = "Modified successfully.";
		const string EditedError = "Sorry for the inconvenience, transaction is failed to save into the database. Please be in touch with admin.";
		const string CreatedError = "Sorry for the inconvenience, couldn't create the last transaction record.";
		const string CreatedSaved = "Transaction is successfully added to the database.";
		const string ControllerName = "Articles";
		///Constant value for where the controller is actually visible.
		const string ControllerVisibleUrl = "";

		#endregion

		#region Enums

		internal enum ViewStates {
            Index,
            Create,
            CreatePostBefore,
            CreatePostAfter,
            Edit,
            EditPostBefore,
            EditPostAfter,
            Details,
            Delete,
            DeletePost
        }

		#endregion

		#region Constructors
		
		public ArticlesController(): base(true){
			ViewBag.controller = ControllerName;
            ViewBag.visibleUrl = ControllerVisibleUrl;
		} 

		#endregion
		
		#region View tapping
		/// <summary>
        /// Always tap once before going into the view.
        /// </summary>
        /// <param name="view">Say the view state, where it is calling from.</param>
        /// <param name="article">Gives the model if it is a editing state or creating posting state or when deleting.</param>
        /// <returns>If successfully saved returns true or else false.</returns>
		bool ViewTapping(ViewStates view, Article article = null, bool entityValidState = true){
			switch (view){
				case ViewStates.Index:
					break;
				case ViewStates.Create:
					break;
				case ViewStates.CreatePostBefore: // before saving it
					break;
                case ViewStates.CreatePostAfter: // after saving
					break;
				case ViewStates.Edit:
					break;
				case ViewStates.Details:
					break;
				case ViewStates.EditPostBefore: // before saving it
					break;
                case ViewStates.EditPostAfter: // after saving
					break;
				case ViewStates.Delete:
					break;
			}
			return true;
		}
		#endregion

		#region Save database common method

		/// <summary>
        /// Better approach to save things into database(than db.SaveChanges()) for this controller.
        /// </summary>
        /// <param name="view">Say the view state, where it is calling from.</param>
        /// <param name="article">Your model information to send in email to developer when failed to save.</param>
        /// <returns>If successfully saved returns true or else false.</returns>
		bool SaveDatabase(ViewStates view, Article article = null){
			// working those at HttpPost time.
			switch (view){
				case ViewStates.Create:
					break;
				case ViewStates.Edit:
					break;
				case ViewStates.Delete:
					break;
			}

			try	{
				var changes = db.SaveChanges(article);
				if(changes > 0){
					return true;
				}
			} catch (Exception ex){
				 throw new Exception("Message : " + ex.Message.ToString() + " Inner Message : " + ex.InnerException.Message.ToString());
			}
			return false;
		}
		#endregion

		#region DropDowns Generate
        [DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetOriginalArticleID() {

            var data = db.Articles.Select(n => new {n.ArticleID, n.Title}).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
		public void GetDropDowns(Article article = null){
			if(article != null){
				ViewBag.OriginalArticleID = new SelectList(db.Articles.ToList(), "ArticleID", "Title", article.OriginalArticleID);
				ViewBag.NextPageArticleID = new SelectList(db.Articles.ToList(), "ArticleID", "Title", article.NextPageArticleID);
				ViewBag.ArticleStateID = new SelectList(db.ArticleStates.ToList(), "ArticleStateID", "State", article.ArticleStateID);
				ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "CategoryID", "CategoryDisplay", article.CategoryID);
				ViewBag.LanguageID = new SelectList(db.Languages.ToList(), "LanguageID", "LanguageID", article.LanguageID);
				ViewBag.FeatureMediaID = new SelectList(db.MediaFiles.ToList(), "MediaFileID", "Title", article.FeatureMediaID);
				ViewBag.WrittenByUserID = new SelectList(db.Users.ToList(), "UserID", "UserName", article.WrittenByUserID);
				ViewBag.LastModifiedByUserID = new SelectList(db.Users.ToList(), "UserID", "UserName", article.LastModifiedByUserID);
				ViewBag.VerifiedByUserID = new SelectList(db.Users.ToList(), "UserID", "UserName", article.VerifiedByUserID);
			} else {			
				ViewBag.OriginalArticleID = new SelectList(db.Articles.ToList(), "ArticleID", "Title");
				ViewBag.NextPageArticleID = new SelectList(db.Articles.ToList(), "ArticleID", "Title");
				ViewBag.ArticleStateID = new SelectList(db.ArticleStates.ToList(), "ArticleStateID", "State");
				ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "CategoryID", "CategoryDisplay");
				ViewBag.LanguageID = new SelectList(db.Languages.ToList(), "LanguageID", "LanguageID");
				ViewBag.FeatureMediaID = new SelectList(db.MediaFiles.ToList(), "MediaFileID", "Title");
				ViewBag.WrittenByUserID = new SelectList(db.Users.ToList(), "UserID", "UserName");
				ViewBag.LastModifiedByUserID = new SelectList(db.Users.ToList(), "UserID", "UserName");
				ViewBag.VerifiedByUserID = new SelectList(db.Users.ToList(), "UserID", "UserName");
			}
		}

		public void GetDropDowns(System.Int64 id){			
				ViewBag.OriginalArticleID = new SelectList(db.Articles.ToList(), "ArticleID", "Title");
				ViewBag.NextPageArticleID = new SelectList(db.Articles.ToList(), "ArticleID", "Title");
				ViewBag.ArticleStateID = new SelectList(db.ArticleStates.ToList(), "ArticleStateID", "State");
				ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "CategoryID", "CategoryDisplay");
				ViewBag.LanguageID = new SelectList(db.Languages.ToList(), "LanguageID", "LanguageID");
				ViewBag.FeatureMediaID = new SelectList(db.MediaFiles.ToList(), "MediaFileID", "Title");
				ViewBag.WrittenByUserID = new SelectList(db.Users.ToList(), "UserID", "UserName");
				ViewBag.LastModifiedByUserID = new SelectList(db.Users.ToList(), "UserID", "UserName");
				ViewBag.VerifiedByUserID = new SelectList(db.Users.ToList(), "UserID", "UserName");
		}
		#endregion

		#region Index
        public ActionResult Index() { 
        
            var articles = db.Articles.Include(a => a.Article2).Include(a => a.Article3).Include(a => a.ArticleState).Include(a => a.Category).Include(a => a.Language).Include(a => a.MediaFile).Include(a => a.User).Include(a => a.User1).Include(a => a.User2);
			var viewOf = ViewTapping(ViewStates.Index);
            return View(articles.ToList());
        }
		#endregion

		#region Index Find - Commented
		/*
        public ActionResult Index(System.Int64 id) {
            var articles = db.Articles.Include(a => a.Article2).Include(a => a.Article3).Include(a => a.ArticleState).Include(a => a.Category).Include(a => a.Language).Include(a => a.MediaFile).Include(a => a.User).Include(a => a.User1).Include(a => a.User2).Where(n=> n. == id);
			bool viewOf = ViewTapping(ViewStates.Index);
            return View(articles.ToList());
        }
		*/
		#endregion

		#region Details
        public ActionResult Details(System.Int64 id) {
        
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
			bool viewOf = ViewTapping(ViewStates.Details, article);
            return View(article);
        }
		#endregion

		#region Create or Add
        public ActionResult Create() {        
			GetDropDowns();
			bool viewOf = ViewTapping(ViewStates.Create);
            return View();
        }

		/*
		public ActionResult Create(System.Int64 id) {        
			GetDropDowns(id); // Generate hidden.
			bool viewOf = ViewTapping(ViewStates.Create);
            return View();
        }
		*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article) {
			ViewTapping(ViewStates.CreatePostBefore, article);
			GetDropDowns(article);
            if (ModelState.IsValid) {            
                db.Articles.Add(article);
                bool state = SaveDatabase(ViewStates.Create, article);
				if (state) {					
					AppVar.SetSavedStatus(ViewBag, CreatedSaved); // Saved Successfully.
				} else {					
					AppVar.SetErrorStatus(ViewBag, CreatedError); // Failed to save
				}
				
                ViewTapping(ViewStates.CreatePostAfter, article,state);
                return View(article);
            }
            ViewTapping(ViewStates.CreatePostAfter, article, false);			
			AppVar.SetErrorStatus(ViewBag, CreatedError); // record is not valid for creation
            return View(article);
        }
		#endregion

        #region Edit or modify record
        public ActionResult Edit(System.Int64 id) {
        
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
			bool viewOf = ViewTapping(ViewStates.Edit, article);
			GetDropDowns(article); // Generating drop downs
            return View(article);
        }

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article) {
			bool viewOf = ViewTapping(ViewStates.EditPostBefore, article);
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                bool state = SaveDatabase(ViewStates.Edit, article);
				if (state) {					
					AppVar.SetSavedStatus(ViewBag, EditedSaved); // Saved Successfully.
				} else {					
					AppVar.SetErrorStatus(ViewBag, EditedError); // Failed to Save
				}
				
                viewOf = ViewTapping(ViewStates.EditPostAfter, article , state);
                return RedirectToAction("Index");
            }
            viewOf = ViewTapping(ViewStates.EditPostAfter, article , false);
        	GetDropDowns(article);
            AppVar.SetErrorStatus(ViewBag, EditedError); // record not valid for save
            return View(article);
        }
		#endregion

		#region Delete or remove record

		
        public ActionResult Delete(long id) {
        
            var article = db.Articles.Find(id);
            bool viewOf = ViewTapping(ViewStates.Delete, article);
			return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]		
        public ActionResult DeleteConfirmed(long id) {
            var article = db.Articles.Find(id);
			bool viewOf = ViewTapping(ViewStates.DeletePost, article);
            db.Articles.Remove(article);
            bool state = SaveDatabase(ViewStates.Delete, article);
			if (!state) {			
				AppVar.SetErrorStatus(ViewBag, DeletedError); // Failed to Save
				return View(article);
			}
			
            return RedirectToAction("Index");
        }
		#endregion

		#region Removing output cache
		public void RemoveOutputCache(string url) {
			HttpResponse.RemoveOutputCacheItem(url);
		}

        public void RemoveOutputCacheOnIndex() {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems(ControllerName, "Index");
            cacheManager.RemoveItems(ControllerName, "List");
            cacheManager = null;
            GC.Collect();
        }
		#endregion
    }

	
}
