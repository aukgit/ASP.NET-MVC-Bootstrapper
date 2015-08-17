using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DevBootstrapper.Models.POCO.IdentityCustomization;

namespace DevBootstrapper.Models.Context {
    public class DevIdentityDbContext : BaseDbContext {
        public DevIdentityDbContext()
            : base("name=DefaultConnection") {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<CoreSetting> CoreSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}