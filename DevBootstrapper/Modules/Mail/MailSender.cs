﻿using System;
using DevMVCComponent;

namespace DevBootstrapper.Modules.Mail {
    public class MailSender {
        private readonly bool _isCompanyNameOnEmailSubject = false;

        public string GetSubject(string sub, string type = "") {
            if (_isCompanyNameOnEmailSubject) {
                if (string.IsNullOrEmpty(type))
                    return "[" + AppVar.Name + "][" + AppVar.Setting.CompanyName + "] " + sub;
                return "[" + AppVar.Name + "][" + AppVar.Setting.CompanyName + "][" + type + "] " + sub;
            }
            if (string.IsNullOrEmpty(type))
                return "[" + AppVar.Name + "] " + sub;
            return "[" + AppVar.Name + "][" + type + "] " + sub;
        }

        public void NotifyAdmin(string subject, string htmlMessage, string type = "", bool generateDecentSubject = true) {
            if (generateDecentSubject) {
                subject = GetSubject(subject, type);
            }
            Starter.Mailer.QuickSend(AppVar.Setting.AdminEmail, subject, htmlMessage);
        }

        /// <summary>
        ///     Notify someone with an email.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="htmlMessage"></param>
        /// <param name="type"></param>
        /// <param name="generateDecentSubject"></param>
        public void Send(string to, string subject, string htmlMessage, string type = "",
            bool generateDecentSubject = true) {
            if (generateDecentSubject) {
                subject = GetSubject(subject, type);
            }
            Starter.Mailer.QuickSend(to, subject, htmlMessage);
        }

        public void NotifyDeveloper(string subject, string htmlMessage, string type = "",
            bool generateDecentSubject = true) {
            if (AppVar.Setting.NotifyDeveloperOnError) {
                if (generateDecentSubject) {
                    subject = GetSubject(subject, type);
                }
                Starter.Mailer.QuickSend(AppVar.Setting.DeveloperEmail, subject, htmlMessage);
            }
        }

        public void NotifyUserMulti(string subject, string toEmail, string[] userEamil, string htmlMessage,
            string type = "", bool generateDecentSubject = true) {
            if (generateDecentSubject) {
                subject = GetSubject(subject, type);
            }
            Starter.Mailer.QuickSend(toEmail, userEamil, subject, htmlMessage);
        }

        public void HandleError(Exception exception, string method, string subject = "", object entity = null,
            string type = "", bool generateDecentSubject = true) {
            {
                if (generateDecentSubject) {
                    subject = GetSubject(subject, type);
                }
                subject += " on method [" + method + "()]";

                Starter.HanldeError.HandleBy(exception, method, subject, entity);
            }
        }
    }
}