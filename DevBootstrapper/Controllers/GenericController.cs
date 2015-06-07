#region using block

using System;
using System.Web.Mvc;
using DevBootstrapper.Modules.Extensions.Context;
using DevMvcComponent.Error;

#endregion

namespace DevBootstrapper.Controllers {
    public abstract class GenericController<TContext> : Controller where TContext : DevDbContext, new() {
        internal ErrorCollector ErrorCollector;
        internal readonly TContext Db;

        protected GenericController() {
        }

        protected GenericController(bool applicationDbContextRequried) {
            if (applicationDbContextRequried) {
                Db = new TContext();
            }
        }

        protected GenericController(bool applicationDbContextRequried, bool errorCollectorRequried) {
            if (errorCollectorRequried) {
                ErrorCollector = new ErrorCollector();
            }
            if (applicationDbContextRequried) {
                Db = new TContext();
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