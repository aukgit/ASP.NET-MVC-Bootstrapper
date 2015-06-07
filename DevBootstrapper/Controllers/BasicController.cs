#region using block

using System;
using System.Web.Mvc;
using DevBootstrapper.Models.Context;

#endregion

namespace DevBootstrapper.Controllers {
    public abstract class BasicController : Controller {
        internal ErrorCollector ErrorCollector;
        internal readonly ApplicationDbContext Db;

        protected BasicController() {
        }

        protected BasicController(bool applicationDbContextRequried) {
            if (applicationDbContextRequried) {
                Db = new ApplicationDbContext();
            }
        }

        protected BasicController(bool applicationDbContextRequried, bool errorCollectorRequried) {
            if (errorCollectorRequried) {
                ErrorCollector = new ErrorCollector();
            }
            if (applicationDbContextRequried) {
                Db = new ApplicationDbContext();
            }
        }

        protected override void Dispose(bool disposing) {
            if (Db != null) {
                Db.Dispose();
            }
            if (ErrorCollector != null) {
                ErrorCollector.Dispose();
            }
            base.Dispose(disposing);
            GC.Collect();
        }
    }
}