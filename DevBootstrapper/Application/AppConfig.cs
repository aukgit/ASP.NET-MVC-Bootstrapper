﻿#region using block

using System;
using System.Linq;
using System.Reflection;
using System.Web;
using DevBootstrapper.Constants;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using DevBootstrapper.Modules.TimeZone;
using DevMvcComponent;
using DevMvcComponent.Error;
using DevMvcComponent.Processor;

#endregion

namespace DevBootstrapper.Application {
    /// <summary>
    ///     Application Configurations, also contains the list of roles.
    /// </summary>
    public static class AppConfig {
        private static CoreSetting _setting;
        private static bool _initalized;
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
                if (_setting == null) {
                    using (var db = new DevIdentityDbContext()) {
                        _setting = db.CoreSettings.FirstOrDefault();
                    }
                }
                return _setting;
            }
        }

        public static ErrorCollector GetNewErrorCollector() {
            return new ErrorCollector();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="force">Forcefully initialize.</param>
        private static void InitalizeDevelopersOrganismComponent(bool force = false) {
            if (!_initalized || force) {
                Config.ApplicationName = AppVar.Name;
                Config.DeveloperEmail = Setting.DeveloperEmail;
                Config.Assembly = Assembly.GetExecutingAssembly();
                Zone.LoadTimeZonesIntoMemory();
                _initalized = true;

                Cookies = Starter.Cookies;
                Caches = Starter.Caches;
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
                    _setting = new CoreSetting {
                        // Set the id to be auto in db.
                        CoreSettingID = 1,
                        ApplicationName = "Developers Organism Component",
                        ApplicationSubtitle = "Subtitle",
                        ApplicationDescription = "Developers Organism component for website maintenance.",
                        CompanyName = "Developers Organism",
                        Language = "English",
                        LiveUrl = "http://www.developers-organism.com",
                        AdminLocation = "Admin",
                        TestingUrl = "http://localhost:port",
                        ServicesControllerUrl  = "/Services/",
                        ApiControllerUrl = "/Api/",
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
                        IsSmptSsl = true,
                        IsFirstUserFound = false
                    };
                    db.CoreSettings.Add(_setting);
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

                _setting = db.CoreSettings.FirstOrDefault();
                if (_setting == null) {
                    throw new Exception("Couldn't create or get the core settings. Please check the creation.");
                }
                InitalizeDevelopersOrganismComponent(true);
                AppVar.IsInTestEnvironment = Setting.IsInTestingEnvironment;

                AppVar.Name = Setting.ApplicationName;
                AppVar.Subtitle = Setting.ApplicationSubtitle;
                AppVar.Setting = Setting;

                AppVar.ApiControllerUrl = Setting.ApiControllerUrl;
                AppVar.ServicesControllerUrl = Setting.ServicesControllerUrl;

                ViewCommon.SetCommonMetaDescriptionToEmpty();
                //Configure this with add a sender email.
                Starter.Mailer = new DevMvcComponent.Mailer.CustomMailConfig(Setting.SenderEmail,
                    Setting.SenderEmailPassword, Setting.SmtpHost, Setting.SmtpMailPort, Setting.IsSmptSsl);
                //if false then no email on error.
                Config.IsNotifyDeveloper = Setting.NotifyDeveloperOnError;

            }
        }

        /// <summary>
        ///     Get error object from session variable and set it to null.
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
        ///     Set Global Error to session variable.
        /// </summary>
        /// <param name="error"></param>
        public static void SetGlobalError(ErrorCollector error) {
            HttpContext.Current.Session[SessionNames.Error] = error;
        }

        #region Public declares

        public static CookieProcessor Cookies;
        public static CacheProcessor Caches;

        public static readonly string[] Roles = {
            "Admin",
            "Moderator",
            "Default",
            "Guest"
        };

        #endregion
    }
}