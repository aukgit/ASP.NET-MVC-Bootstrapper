#region using block

using DevBootstrapper.Application;
using FluentScheduler;

#endregion

namespace DevBootstrapper.Scheduler {
    internal class SiteRunnerScheduler : ITask {
        #region ITask Members

        private bool _test = AppVar.IsInTestEnvironment;

        public void Execute() {
            // keep the app running
            if (AppVar.Url != null) {

            }
        }

        #endregion
    }
}