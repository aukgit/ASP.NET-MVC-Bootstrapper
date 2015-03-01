using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DevMVCComponent;
using DevMVCComponent.Mailers;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using DevBootstrapper.Modules.Cache;
using DevBootstrapper.Modules.Cookie;
using DevBootstrapper.Modules.Mail;
using DevBootstrapper.Modules.Session;
using DevBootstrapper.Modules.TimeZone;
using DevBootstrapper.Modules.UserError;

namespace DevBootstrapper {
    /// <summary>
    ///     Application Configurations, also contains the list of roles.
    /// </summary>
    public static class AppConfig {
        private static CoreSetting setting;
        private static bool initalized;
        private static int _truncateLength = 30;

        public static int ValidationMaxNumber {
            get { return 10; }
        }

        public static int TruncateLength {
            get { return _truncateLength; }
            set { _truncateLength = value; }
        }

        /// <summary>
        ///     Get few common classes from Developers Organism Component.
        /// </summary>
        public static CoreSetting Setting {
            get {
                if (setting == null) {
                    using (var db = new DevIdentityDbContext()) {
                        setting = db.CoreSettings.FirstOrDefault();
                    }
                }
                return setting;
            }
        }

        public static ErrorCollector GetNewErrorCollector() {
            return new ErrorCollector();
        }

        private static void InitalizeDevelopersOrganismComponent(bool force = false) {
            if (!initalized || force) {
                Config.ApplicationName = AppVar.Name;
                Config.AdminEmail = Setting.AdminEmail;
                Config.DeveloperEmail = Setting.DeveloperEmail;
                Config.Assembly = Assembly.GetExecutingAssembly();
                Zone.LoadTimeZonesIntoMemory();
                initalized = true;
            }
        }

        /// <summary>
        ///     Settings will not be null. Default values will be pushed.
        /// </summary>
        /// <returns></returns>
        public static bool CreateDefaultCoreSetting() {
            var s = Setting;
            if (s == null) {
                //no setting exist , need to create a default setting.
                using (var db = new DevIdentityDbContext()) {
                    setting = new CoreSetting {
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
                    db.CoreSettings.Add(setting);
                    var i = db.SaveChanges();
                    if (i >= 0) {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public static void RefreshSetting() {
            using (var db = new DevIdentityDbContext()) {
                CreateDefaultCoreSetting();

                setting = db.CoreSettings.FirstOrDefault();
                if (setting == null) {
                    throw new Exception("Couldn't create or get the core settings. Please check the creation.");
                }
                InitalizeDevelopersOrganismComponent(true);
                AppVar.IsInTestEnvironment = Setting.IsInTestingEnvironment;

                AppVar.Name = Setting.ApplicationName;
                AppVar.Subtitle = Setting.ApplicationSubtitle;
                AppVar.Setting = Setting;
                AppVar.SetCommonMetaDescriptionToEmpty();
                //Configure this with add a sender email.
                Starter.Mailer = new CustomMailConfig(Setting.SenderEmail, Setting.SenderEmailPassword, Setting.SmtpHost,
                    Setting.SmtpMailPort, Setting.IsSMTPSSL);
                //if false then no email on error.
                Config.IsNotifyDeveloper = Setting.NotifyDeveloperOnError;
            }
        }

        /// <summary>
        ///     Get error and set it to null.
        /// </summary>
        /// <returns></returns>
        public static ErrorCollector GetGlobalError() {
            if (HttpContext.Current.Session[SessionNames.Error] != null) {
                var error = (ErrorCollector)HttpContext.Current.Session[SessionNames.Error];
                HttpContext.Current.Session[SessionNames.Error] = null;
                return error;
            }
            return null;
        }

        /// <summary>
        ///     Set Global Error
        /// </summary>
        /// <param name="error"></param>
        public static void SetGlobalError(ErrorCollector error) {
            HttpContext.Current.Session[SessionNames.Error] = error;
        }

        #region Public declares

        public static CookieProcessor Cookies = new CookieProcessor();
        public static CacheProcessor Caches = new CacheProcessor();
        public static ErrorCollector ErrorCollection = new ErrorCollector();

        public static readonly string[] Roles = {
            "Admin",
            "Moderator",
            "Default",
            "Guest"
        };

        #endregion
    }

    /// <summary>
    ///     Application Global Variables
    /// </summary>
    public struct AppVar {
        #region Enums

        #endregion

        #region Constants

        #endregion

        #region Connection Strings and Constants

        public const string DefaultConnection =
            @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\asp.net-Maganize.mdf;Initial Catalog=asp.net-Maganize;Integrated Security=True";

        public enum ConnectionStringType {
            DefaultConnection,
            Secondary
        };

        #endregion

        #region Propertise

        private static string _productNameMeta;

        /// <summary>
        ///     Application Name
        /// </summary>
        public static string Name;

        /// <summary>
        ///     Application Subtitle
        /// </summary>
        public static string Subtitle;

        /// <summary>
        ///     Is application in testing environment or not?
        /// </summary>
        public static bool IsInTestEnvironment;


        public static CoreSetting Setting;

        /// <summary>
        ///     Get the application URL based on the application environment.
        ///     Without slash.
        /// </summary>
        public static string Url {
            get {
                if (IsInTestEnvironment) {
                    return AppConfig.Setting.TestingUrl;
                }
                return AppConfig.Setting.LiveUrl;
            }
        }

        public static MailSender Mailer = new MailSender();

        #endregion

        #region Functions

        public static string GetConnectionString(ConnectionStringType type) {
            switch (type) {
                case ConnectionStringType.DefaultConnection:
                    return DefaultConnection;
                case ConnectionStringType.Secondary:
                    break;
                default:
                    break;
            }
            return null;
        }

        private static string getCommonMetadescription() {
            var finalMeta = "";
            if (_productNameMeta == null) {
                var nameList = Name.Split(' ').ToList();
                nameList.Add(Name);
                nameList.Add(Subtitle);
                foreach (var item in nameList) {
                    if (finalMeta.Equals("")) {
                        finalMeta += ",";
                    }
                    finalMeta += item;
                }
                _productNameMeta = finalMeta;
            }
            return _productNameMeta;
        }

        internal static void SetCommonMetaDescriptionToEmpty() {
            _productNameMeta = null;
        }

        public static ActionResult GetFriendlyError(string title, string message) {
            var dictionary = new ViewDataDictionary {
                {"Title", title},
                {"ErrorMessage", message}
            };
            return new ViewResult {
                ViewName = "_FriendlyError",
                ViewData = dictionary
            };
        }

        public static ActionResult GetAuthenticationError(string title, string message) {
            var dictionary = new ViewDataDictionary {
                {"Title", title},
                {"ErrorMessage", message}
            };
            return new ViewResult {
                ViewName = "_AuthenticationError",
                ViewData = dictionary
            };
        }

        public static void GetTitlePageMeta(dynamic viewBag, string title, string msg = "", string meta = null,
            string keywords = null) {
            viewBag.Title = title;
            viewBag.Message = msg;
            viewBag.Meta = meta + "," + getCommonMetadescription();
            viewBag.Keywords = keywords + "," + getCommonMetadescription();
        }

        public static void SetSavedStatus(dynamic viewBag, string msg = null) {
            if (msg == null) {
                msg = "Your previous transaction is successfully saved.";
            }
            viewBag.Success = msg;
        }

        public static void SetErrorStatus(dynamic viewBag, string msg = null) {
            if (msg == null) {
                msg = "Your last transaction is not saved.";
            }
            viewBag.ErrorSave = msg;
        }

        #endregion
    }
}