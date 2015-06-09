#region using block

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using DevBootstrapper.Modules.Mail;

#endregion

namespace DevBootstrapper.Application {
    /// <summary>
    ///     Application related changeable variables
    /// </summary>
    public struct AppVar {

        #region Propertise
        public static MailSender Mailer = new MailSender();

        private static readonly Dictionary<string, string> ConfigList = new Dictionary<string, string>(30);

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
        public static string Url
        {
            get
            {
                if (IsInTestEnvironment) {
                    return AppConfig.Setting.TestingUrl;
                }
                return AppConfig.Setting.LiveUrl;
            }
        }


        #endregion


        /// <summary>
        /// Get config value by configName
        /// </summary>
        /// <param name="configName">Give a config name</param>
        /// <returns>Returns value as string, if not found returns empty string. No execption.</returns>
        public string GetConfig(string configName) {
            if (string.IsNullOrEmpty(configName)) {
                return "";
            }
            if (ConfigList.ContainsKey(configName)) {
                // if cache exist
                var cachedConfigValue = ConfigList[configName];
                return cachedConfigValue;
            }
            var appIdConfig =
                WebConfigurationManager.AppSettings[configName];
            if (appIdConfig != null && appIdConfig.Length >= 1) {
                var value = appIdConfig.FirstOrDefault().ToString();
                ConfigList.Add(configName, value);
                return value;
            }
            return "";

        }
  
    }
}