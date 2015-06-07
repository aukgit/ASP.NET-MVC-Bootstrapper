#region using block

using DevBootstrapper;
using Microsoft.Owin;
using Owin;

#endregion

[assembly: OwinStartup(typeof (Startup))]

namespace DevBootstrapper {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}