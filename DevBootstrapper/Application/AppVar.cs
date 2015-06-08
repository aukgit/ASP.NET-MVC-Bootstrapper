﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using DevBootstrapper.Modules.Mail;

namespace DevBootstrapper.Application {
    /// <summary>
    /// Application related changeable variables
    /// </summary>
    public struct AppVar {

        #region Enums

        #endregion

        #region Constants

        #endregion

        #region Connection Strings and Constants
        //public const string DefaultConnection = @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\DevBootstrapper-Accounts.mdf;Initial Catalog=DevBootstrapper-Accounts;Integrated Security=True";

        private static readonly string DefaultConnection =
             ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public enum ConnectionStringType {
            DefaultConnection,
            Secondary
        };


        #endregion

        #region Propertise
        static string _productNameMeta;
        /// <summary>
        /// Application Name
        /// </summary>
        public static string Name;
        /// <summary>
        /// Application Subtitle
        /// </summary>
        public static string Subtitle;
        /// <summary>
        /// Is application in testing environment or not?
        /// </summary>
        public static bool IsInTestEnvironment;



        public static CoreSetting Setting;
        /// <summary>
        /// Get the application URL based on the application environment.
        /// Without slash.
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
        static string GetCommonMetadescription() {
            string finalMeta = "";
            if (_productNameMeta == null) {
                var nameList = AppVar.Name.Split(' ').ToList();
                nameList.Add(AppVar.Name);
                nameList.Add(AppVar.Subtitle);
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
            var dictionary = new ViewDataDictionary(){
              {"Title",title},
              {"ErrorMessage",message}
            };
            return new ViewResult() {
                ViewName = "_FriendlyError",
                ViewData = dictionary
            };
        }

        public static ActionResult GetAuthenticationError(string title, string message) {
            var dictionary = new ViewDataDictionary(){
              {"Title", title},
              {"ErrorMessage",message}
            };
            return new ViewResult() {
                ViewName = "_AuthenticationError",
                ViewData = dictionary
            };
        }

        public static void GetTitlePageMeta(dynamic viewBag, string title, string msg = "", string meta = null, string keywords = null) {
            viewBag.Title = title;
            viewBag.Message = msg;
            viewBag.Meta = meta + "," + GetCommonMetadescription();
            viewBag.Keywords = keywords + "," + GetCommonMetadescription();
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