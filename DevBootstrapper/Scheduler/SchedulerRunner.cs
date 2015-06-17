#region using block

using FluentScheduler;

#endregion

namespace DevBootstrapper.Scheduler {
    public class SchedulerRunner : Registry {
        public SchedulerRunner() {
            // keep the site running in the pool
            Schedule<SiteRunnerScheduler>().ToRunNow().AndEvery(2).Minutes();
            // load home page at every hour.
            Schedule<SiteVisitingScheduler>().ToRunNow().AndEvery(1).Hours();
        }
    }
}