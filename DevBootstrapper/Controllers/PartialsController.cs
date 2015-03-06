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
    public class PartialsController : GenericController<NorthwindEntities> {
        
        
		#region Constructors

        public PartialsController()
            : base(true) {
	
		} 

		#endregion

        #region EmployeesController : DropDowns to paste into the partial

        // [DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetReportsTo() {
            var data = db.Employees.Select(n => new { id = n.EmployeeID, display = n.LastName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ProductOrdersController : DropDowns to paste into the partial

        // [DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetCustomerID() {
            var data = db.Customers.Select(n => new { id = n.CustomerID, display = n.CompanyName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // [DonutOutputCache(CacheProfile = "YearNoParam")]
        public JsonResult GetShipVia(int id) {
            var data = db.Shippers.Where(n=> n.ShipperID == id).Select(n => new { id = n.ShipperID, display = n.CompanyName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
 
    }
	
}
