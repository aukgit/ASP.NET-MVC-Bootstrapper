using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DevBootstrapper.Models.EntityModel.POCO;
using DevBootstrapper.Modules.Extensions.Context;
using TimeZone = DevBootstrapper.Models.EntityModel.POCO.TimeZone;

namespace DevBootstrapper.Models.Context
{
    public partial class MagazineEntities : DevDbContext
    {
        public MagazineEntities()
            : base("name=MagazineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Advertise> Advertises { get; set; }
        public virtual DbSet<AdvertisePaymentConfirmed> AdvertisePaymentConfirmeds { get; set; }
        public virtual DbSet<AdvertiseRequest> AdvertiseRequests { get; set; }
        public virtual DbSet<AdvertiseType> AdvertiseTypes { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleState> ArticleStates { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DraftLog> DraftLogs { get; set; }
        public virtual DbSet<FileType> FileTypes { get; set; }
        public virtual DbSet<Keyword> Keywords { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<MediaArticleRelation> MediaArticleRelations { get; set; }
        public virtual DbSet<ModifyLog> ModifyLogs { get; set; }
        public virtual DbSet<MoneyObtained> MoneyObtaineds { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<NotifiedRelation> NotifiedRelations { get; set; }
        public virtual DbSet<Notify> Notifies { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<ProfileBasedSearch> ProfileBasedSearches { get; set; }
        public virtual DbSet<RelatedArticle> RelatedArticles { get; set; }
        public virtual DbSet<SalaryPaid> SalaryPaids { get; set; }
        public virtual DbSet<SalaryType> SalaryTypes { get; set; }
        public virtual DbSet<SendArticle> SendArticles { get; set; }
        public virtual DbSet<TimeZone> TimeZones { get; set; }
        public virtual DbSet<TopSearch> TopSearches { get; set; }
        public virtual DbSet<URL> URLs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<XPGainedType> XPGainedTypes { get; set; }
        public virtual DbSet<ViewSearchingArticle> ViewSearchingArticles { get; set; }
    }
}
