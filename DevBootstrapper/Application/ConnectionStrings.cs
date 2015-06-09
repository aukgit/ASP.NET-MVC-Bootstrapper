using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DevBootstrapper.Application {
    public class ConnectionStrings {
        private static readonly Dictionary<string, string> ConfigList = new Dictionary<string, string>(30);

        #region Connection Strings and Constants

        //public const string DefaultConnection = @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\DevBootstrapper-Accounts.mdf;Initial Catalog=DevBootstrapper-Accounts;Integrated Security=True";

        private static readonly string DefaultConnection =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public enum ConnectionStringType {
            DefaultConnection,
            Secondary
        };

        #endregion


        public static string Get(ConnectionStringType type) {
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

        /// <summary>
        /// Get config value by configName
        /// </summary>
        /// <param name="configName">Give a config name</param>
        /// <returns>Returns value as string, if not found returns empty string. No execption.</returns>
       
        public static string Get(string configName) {
            if (string.IsNullOrEmpty(configName)) {
                return "";
            }
            if (ConfigList.ContainsKey(configName)) {
                // if cache exist
                var cachedConfigValue = ConfigList[configName];
                return cachedConfigValue;
            }
            var appIdConfig =
                ConfigurationManager.ConnectionStrings[configName];
            if (appIdConfig != null) {
                var value = appIdConfig.ConnectionString;
                ConfigList.Add(configName, value);
                return value;
            }
            return "";
        }


    }
}