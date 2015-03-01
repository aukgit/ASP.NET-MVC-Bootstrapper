using System.Web.Mvc;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Modules.UserError;

namespace DevBootstrapper.Controllers {
    //[CompressFilter(Order=1)]
    //[CacheFilter(Duration=8)]
    public abstract class BasicController : Controller {
        internal ErrorCollector _errorCollector;
        internal readonly ApplicationDbContext db;

        public BasicController() {
        }

        public BasicController(bool applicationDbContextRequried) {
            if (applicationDbContextRequried) {
                db = new ApplicationDbContext();
            }
        }

        public BasicController(bool applicationDbContextRequried, bool errorCollectorRequried) {
            if (errorCollectorRequried) {
                _errorCollector = new ErrorCollector();
            }
            if (applicationDbContextRequried) {
                db = new ApplicationDbContext();
            }
        }

        protected override void Dispose(bool disposing) {
            if (db != null) {
                db.Dispose();
            }
            if (_errorCollector != null) {
                _errorCollector.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}