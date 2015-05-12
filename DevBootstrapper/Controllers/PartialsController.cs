using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevBootstrapper.Controllers;
using DevBootstrapper.Models.Context;
//using DevBootstrapper.Models.EntityModel.POCO; // Northwind Sample
using DevTrends.MvcDonutCaching;
using DevBootstrapper.Filter;
using DevBootstrapper.Helpers;
using DevBootstrapper.Modules.Cache;
using DevBootstrapper.Modules.Session;

namespace DevBootstrapper.Controllers
{
    [OutputCache(CacheProfile = "YearNoParam")]
    [CacheFilterAttribute]
    //public class PartialsController : GenericController<Inherit it with your db context> {
    public class PartialsController : GenericController<NorthwindEntities> {
        
        
		#region Constructors

        public PartialsController()
            : base(true) {
	
		} 

		#endregion

        #region Drop down : Country, timezone, language
        [OutputCache(CacheProfile = "YearNoParam")]
        public string GetCountryId() {
            var countries = CachedQueriedData.GetCountries();
            return HtmlHelpers.DropDownCountry(countries);
        }

        [OutputCache(CacheProfile = "Day", VaryByParam = "id")]
        public ActionResult GetTimeZone(int id) {
            if (SessionNames.IsValidationExceed("GetTimeZone", 100)) {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var getZones = CachedQueriedData.GetTimezones(id);
            if (getZones != null) {
                var represent = getZones.Select(n => new { display = n.Display, id = n.UserTimeZoneID });
                return Json(represent.ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(CacheProfile = "Day", VaryByParam = "id")]
        public ActionResult GetLanguage(int id) {
            if (SessionNames.IsValidationExceed("GetLanguage", 100)) {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var languges = CachedQueriedData.GetLanguages(id);
            if (languges != null) {
                var represent =
                    languges.Select(n => new { display = n.Language + " - " + n.NativeName, id = n.CountryLanguageID });
                return Json(represent.ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Nortwind test

        #region EmployeesController : DropDowns to paste into the partial

        // [DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetReportsTo() {
            var data = db.Employees.Select(n => new { id = n.EmployeeID, display = n.LastName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ProductOrdersController : DropDowns to paste into the partial
        public JsonResult GetEmployeeID() {
            var data = db.Employees.Select(n => new { id = n.EmployeeID, display = n.LastName + " (" + n.EmployeeID + ")" }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        

        [OutputCache(CacheProfile = "Year")]
        public JsonResult GetShipVia(int id) {
            var data = db.Shippers.Where(n => n.ShipperID == id).Select(n => new { id = n.ShipperID, display = n.CompanyName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion 
        #endregion
 
    }
	
}
