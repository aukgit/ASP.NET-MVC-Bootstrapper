using System.Web.Mvc;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.EntityModel;
using DevBootstrapper.Modules.UserError;

namespace DevBootstrapper.Controllers {

    public abstract class AdvanceController : Controller {
        internal ErrorCollector ErrorCollector;
        internal readonly MagazineEntities db;

        protected AdvanceController() {
        }

        protected AdvanceController(bool applicationDbContextRequried) {
            if (applicationDbContextRequried) {
                db = new MagazineEntities();
            }
        }

        protected AdvanceController(bool applicationDbContextRequried, bool errorCollectorRequried) {
            if (errorCollectorRequried) {
                ErrorCollector = new ErrorCollector();
            }
            if (applicationDbContextRequried) {
                db = new MagazineEntities();
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