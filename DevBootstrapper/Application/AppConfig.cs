using System;
using System.Linq;
using System.Web;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using DevBootstrapper.Modules.Session;
using DevBootstrapper.Modules.TimeZone;
using DevBootstrapper.Modules.UserError;
using DevMvcComponent.Processor;

namespace DevBootstrapper.Application {
    /// <summary>
    /// Application Configurations, also contains the list of roles.
    /// </summary>
    public static class AppConfig {

        #region Public declares

        public static CookieProcessor Cookies;
        public static CacheProcessor Caches;
        public static ErrorCollector ErrorCollection = new ErrorCollector();
        public static readonly string[] Roles = new string[] {
            "Admin",
            "Moderator",
            "Default",
            "Guest"
        };

        #endregion
        public static ErrorCollector GetNewErrorCollector() {
            return new ErrorCollector();
        }
        private static CoreSetting _setting = null;
        private static bool _initalized = false;
        private static int _truncateLength = 30;

        public static int ValidationMaxNumber { get { return 10; } }

        public static int TruncateLength {
            get {
                return _truncateLength;
            }
            set {
                _truncateLength = value;
            }
        }


        private static void InitalizeDevelopersOrganismComponent(bool force = false) {
            if (!_initalized || force) {
                DevMvcComponent.Config.ApplicationName = AppVar.Name;
                DevMvcComponent.Config.AdminEmail = Setting.AdminEmail;
                DevMvcComponent.Config.DeveloperEmail = Setting.DeveloperEmail;
                DevMvcComponent.Config.Assembly = System.Reflection.Assembly.GetExecutingAssembly();
                Zone.LoadTimeZonesIntoMemory();
                _initalized = true;
            }
        }

        /// <summary>
        /// Get few common classes from Developers Organism Component.
        /// </summary>


        public static CoreSetting Setting {
            get {
                if (_setting == null) {
                    using (DevIdentityDbContext db = new DevIdentityDbContext()) {
                        _setting = db.CoreSettings.FirstOrDefault();
                    }
                }
                return _setting;
            }
        }

        /// <summary>
        /// Settings will not be null. Default values will be pushed.
        /// </summary>
        /// <returns></returns>
        public static bool CreateDefaultCoreSetting() {
            var s = Setting;
            if (s == null) {
                //no setting exist , need to create a default setting.
                using (DevIdentityDbContext db = new DevIdentityDbContext()) {
                    _setting = new CoreSetting() {
                        // Set the id to be auto in db.
                        CoreSettingID = 1,
                        ApplicationName = "Developers Organism Component",
                        ApplicationSubtitle = "Subtitle",
                        ApplicationDescription = "Developers Organism component for website maintenance.",
                        CompanyName = "Developers Organism",
                        Language = "English",
                        LiveUrl = "http://www.developers-organism.com",
                        AdminLocation = "Admin",
                        TestingUrl = "http://localhost:port/",
                        AdminEmail = "devorg.bd@gmail.com",
                        DeveloperEmail = "devorg.bd@gmail.com",
                        OfficePhone = 018,
                        Fax = 018,
                        MarketingEmail = "devorg.bd@gmail.com",
                        SupportEmail = "devorg.bd@gmail.com",
                        MarketingPhone = 018,
                        SupportPhone = 018,
                        IsAuthenticationEnabled = false,
                        IsInTestingEnvironment = true,
                        DoesRegisterCodeNeverExpires = true,
                        IsRegisterCodeRequiredToRegister = false,
                        ShouldRegistrationCodeBeLinkedWithUser = true,
                        SenderEmailPassword = "123",
                        Address = "Address of your office.",
                        OfficeEmail = "info@developers-organism.com",
                        SenderEmail = "YourSender@Email.com",
                        SenderDisplay = "Your sender display",
                        SmtpHost = "smtp.gmail.com",
                        SmtpMailPort = 587,
                        GoogleMetaTag = "Meta tag",
                        FacebookClientID = 123,
                        FacebookSecret = "FB App Secret",
                        IsFacebookAuthentication = true,
                        NotifyDeveloperOnError = true,
                        IsConfirmMailRequired = true,
                        IsSMTPSSL = true,
                        IsFirstUserFound = false
                    };
                    db.CoreSettings.Add(_setting);
                    int i = db.SaveChanges();
                    if (i >= 0) {
                        return true;
                    } else {
                        return false;
                    }
                }
            }
            return false;
        }

        public static void RefreshSetting() {

            using (var db = new DevIdentityDbContext()) {
                CreateDefaultCoreSetting();

                _setting = db.CoreSettings.FirstOrDefault();
                if (_setting == null) {
                    throw new Exception("Couldn't create or get the core settings. Please check the creation.");
                }
                InitalizeDevelopersOrganismComponent(true);
                AppVar.IsInTestEnvironment = Setting.IsInTestingEnvironment;

                AppVar.Name = Setting.ApplicationName.ToString();
                AppVar.Subtitle = Setting.ApplicationSubtitle.ToString();
                AppVar.Setting = Setting;
                AppVar.SetCommonMetaDescriptionToEmpty();
                //Configure this with add a sender email.
                DevMvcComponent.Starter.Mailer = new DevMvcComponent.Mailers.CustomMailConfig(Setting.SenderEmail, Setting.SenderEmailPassword, Setting.SmtpHost, Setting.SmtpMailPort, Setting.IsSMTPSSL);
                //if false then no email on error.
                DevMvcComponent.Config.IsNotifyDeveloper = Setting.NotifyDeveloperOnError;

            }
        }

        /// <summary>
        /// Get error and set it to null.
        /// </summary>
        /// <returns></returns>
        public static ErrorCollector GetGlobalError() {
            if (HttpContext.Current.Session[SessionNames.Error] != null) {
                var error = (ErrorCollector)HttpContext.Current.Session[SessionNames.Error];
                HttpContext.Current.Session[SessionNames.Error] = null;
                return error;
            } else {
                return null;
            }
        }

        /// <summary>
        /// Set Global Error
        /// </summary>
        /// <param name="error"></param>
        public static void SetGlobalError(ErrorCollector error) {
            HttpContext.Current.Session[SessionNames.Error] = error;
        }

    }
   
}