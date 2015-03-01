using System.Web.Mvc;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Modules.UserError;

namespace DevBootstrapper.Controllers {
    //[CompressFilter(Order=1)]
    //[CacheFilter(Duration=8)]
    public abstract class BasicController : Controller {
<<<<<<< HEAD
        internal ErrorCollector ErrorCollector;
        internal readonly ApplicationDbContext Db;
=======
        internal ErrorCollector _errorCollector;
        internal readonly ApplicationDbContext db;
>>>>>>> parent of c7c6039... complete and okay for first time

        public BasicController() {
        }

        public BasicController(bool applicationDbContextRequried) {
            if (applicationDbContextRequried) {
                Db = new ApplicationDbContext();
            }
        }

        public BasicController(bool applicationDbContextRequried, bool errorCollectorRequried) {
            if (errorCollectorRequried) {
                _errorCollector = new ErrorCollector();
            }
            if (applicationDbContextRequried) {
                Db = new ApplicationDbContext();
            }
        }

        protected override void Dispose(bool disposing) {
            if (Db != null) {
                Db.Dispose();
            }
            if (_errorCollector != null) {
                _errorCollector.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}