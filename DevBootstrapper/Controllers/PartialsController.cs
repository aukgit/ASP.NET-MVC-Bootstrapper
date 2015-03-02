using System;
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
    [DonutOutputCache(CacheProfile = "YearNoParam")]
    public class PartialsController : GenericController<MagazineEntities> {
        
        
		#region Constructors

        public PartialsController()
            : base(true) {
	
		} 

		#endregion
        #region ArticleController

        //[DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetOriginalArticleID() {
            var data = db.Articles.Select(n => new { id = n.ArticleID, value = n.Title }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetNextPageArticleID() {
            var data = db.Articles.Select(n => new { id = n.ArticleID, value = n.Title }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetArticleStateID() {
            var data = db.ArticleStates.Select(n => new { id = n.ArticleStateID, value = n.State }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetCategoryID() {
            var data = db.Categories.Select(n => new { id = n.CategoryID, value = n.CategoryDisplay }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetLanguageID() {
            var data = db.Languages.Select(n => new { id = n.LanguageID, value = n.LanguageID }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetFeatureMediaID() {
            var data = db.MediaFiles.Select(n => new { id = n.MediaFileID, value = n.Title }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetWrittenByUserID() {
            var data = db.Users.Select(n => new { id = n.UserID, value = n.UserName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetLastModifiedByUserID() {
            var data = db.Users.Select(n => new { id = n.UserID, value = n.UserName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetVerifiedByUserID() {
            var data = db.Users.Select(n => new { id = n.UserID, value = n.UserName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
	
}
