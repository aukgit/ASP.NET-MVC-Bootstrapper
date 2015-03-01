using System.Web.Mvc;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Modules.UserError;

namespace DevBootstrapper.Controllers {

    public abstract class BasicController : Controller {
        internal ErrorCollector ErrorCollector;
        internal readonly ApplicationDbContext db;

        protected BasicController() {
        }

        protected BasicController(bool applicationDbContextRequried) {
            if (applicationDbContextRequried) {
                db = new ApplicationDbContext();
            }
        }

        protected BasicController(bool applicationDbContextRequried, bool errorCollectorRequried) {
            if (errorCollectorRequried) {
                ErrorCollector = new ErrorCollector();
            }
            if (applicationDbContextRequried) {
                db = new ApplicationDbContext();
            }
        }

        protected override void Dispose(bool disposing) {
            if (db != null) {
                db.Dispose();
            }
            if (ErrorCollector != null) {
                ErrorCollector.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}