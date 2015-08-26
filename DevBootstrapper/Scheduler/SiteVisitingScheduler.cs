using DevBootstrapper.Application;
using FluentScheduler;

namespace DevBootstrapper.Scheduler {
    internal class SiteVisitingScheduler : ITask {
        #region ITask Members

        private bool _test = AppVar.IsInTestEnvironment;

        public void Execute() {
            // keep the app running
            var contents = new System.Net.WebClient().DownloadString(AppVar.Url);
        }

        #endregion
    }
}