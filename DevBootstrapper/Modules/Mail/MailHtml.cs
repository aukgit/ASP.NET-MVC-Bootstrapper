using System.Text;
using DevBootstrapper.Models.POCO.Identity;
using DevBootstrapper.Modules.Cache;

namespace DevBootstrapper.Modules.Mail {
    public static class MailHtml {
        #region Propertise

        private static StringBuilder Sb {
            get {
                if (_sb != null) {
                    return _sb;
                }
                _sb = new StringBuilder(Len);
                return _sb;
            }
        }

        #endregion

        public static string ThanksFooter(string footerSenderName, string department) {
            var s = string.Format(DivTag, "", "Thank you", "Thank  you,");
            s += string.Format(DivTag, "font-size:14px;font-weight:bold;", "", footerSenderName);
            s += string.Format(DivTag, "", "Department", department);
            s += string.Format(DivTag, "", AppVar.Name, AppVar.Name);
            s += string.Format(DivTag, "", AppVar.Subtitle, AppVar.Subtitle);
            return s;
        }

        public static string EmailConfirmHtml(ApplicationUser user, string callBackUrl, string footerSenderName = "",
            string department = "Administration", string body = null) {
            Sb.Clear();
            if (body == null) {
                body = string.Format(DefaultMailConfirmBody, AppVar.Url, AppVar.Name, callBackUrl, callBackUrl);
            }

            AddGreetingsToStringBuilder(user);
            Sb.AppendLine(body);
            Sb.AppendLine(LineBreak);
            Sb.AppendLine(string.Format(DivTag, "", "", "Name : " + user.DisplayName));
            Sb.AppendLine(string.Format(DivTag, "", "", "Login(username) : " + user.UserName));
            Sb.AppendLine(string.Format(DivTag, "", "", "Email : " + user.Email));
            Sb.AppendLine(string.Format(DivTag, "", "", "Phone : " + user.PhoneNumber));
            Sb.AppendLine(string.Format(DivTag, "", "", "Timezone : " + CachedQueriedData.GetTimezone(user).UtcName));
            Sb.AppendLine(string.Format(DivTag, "", "", "Country : " + CachedQueriedData.GetCountry(user).CountryName));
            Sb.AppendLine(LineBreak);
            Sb.AppendLine(ThanksFooter(AppVar.Setting.AdminName, department));
            return Sb.ToString();
        }

        /// <summary>
        ///     Usages line break after greetings
        /// </summary>
        /// <param name="user"></param>
        /// <param name="showFullName">Full name gives First+ ' ' + LastName</param>
        public static void AddGreetingsToStringBuilder(ApplicationUser user, bool showFullName = false) {
            if (showFullName) {
                Sb.AppendLine("Hello " + user.LastName + ", <br>");
            } else {
                Sb.AppendLine("Hello " + user.DisplayName + ", <br>");
            }
            Sb.AppendLine(LineBreak);
        }

        /// <summary>
        ///     Don't give a line break. Use your own.
        /// </summary>
        public static void AddContactUsToStringBuilder() {
            Sb.AppendLine("If you have any further query, we would love to hear it. Please drop your feedbacks at " +
                          GetContactUsLinkHtml() + ".");
        }

        public static string GetContactUsLinkHtml(string linkName = null, string title = null, string addClass = "") {
            if (linkName == null) {
                linkName = AppVar.Url + "/ContactUs";
                linkName = linkName.Replace("http://", "");
            }
            if (title == null) {
                title = "Contact us and drop your feedback about anything.";
            }
            return string.Format(ContactUsLink, title, linkName, addClass, AppVar.Url);
        }

        public static string BlockEmailHtml(ApplicationUser user, string reasonForBlocking, string footerSenderName = "",
            string department = "Administration", string body = null) {
            Sb.Clear();

            AddGreetingsToStringBuilder(user); // greetings

            Sb.AppendLine("You have been blocked from " + AppVar.Name + ".<br>");
            Sb.AppendLine("Reason : " + reasonForBlocking + ".<br>");
            Sb.AppendLine(LineBreak);

            AddContactUsToStringBuilder(); //contact us

            Sb.AppendLine(ThanksFooter(footerSenderName, department));
            return Sb.ToString();
        }

        public static string ReleasedFromBlockEmailHtml(ApplicationUser user, string footerSenderName = "",
            bool saySorry = false, string department = "Administration", string body = null) {
            Sb.Clear();

            AddGreetingsToStringBuilder(user); // greetings

            Sb.AppendLine("You have been re-enabled again in " + AppVar.Name + ".");
            if (saySorry) {
                Sb.AppendLine("We are deeply sorry for your inconvenience.");
            }

            Sb.AppendLine(LineBreak);
            AddContactUsToStringBuilder(); //contact us
            Sb.AppendLine(LineBreak);
            Sb.AppendLine(ThanksFooter(footerSenderName, department));
            return Sb.ToString();
        }

        #region Declaration

        private const int Len = 2000;

        /// <summary>
        ///     We are very delighted to have you in [a href='{0}' title='{1}']{1}[/a]. [a href='{2}' title='{3}']Here[/a] is the
        ///     [a href='{2}' title='{3}']link[/a] to active your account. Or you can also copy paste the raw version below to your
        ///     browser's address bar. Raw : {3}
        /// </summary>
        private const string DefaultMailConfirmBody =
            "We are very delighted to have you in <a href='{0}' title='{1}'>{1}</a>. <a href='{2}' title='{3}'>Here</a> is the <a href='{2}' title='{3}'>link</a> to active your account. Or you can also copy paste the raw version below to your browser's address bar.<br><br> Raw : {3} <br><br>";


        private static StringBuilder _sb;

        #endregion

        #region HTMl Tag Constants

        /// <summary>
        ///     [a id='contact-us-page-link' class='contact-us-page-link' href='{3}/ContactUs' class='{2}' title='{0}']{1}[/a]
        /// </summary>
        internal const string ContactUsLink =
            "<a id='contact-us-page-link' class='contact-us-page-link' href='{3}/ContactUs' class='{2}' title='{0}'>{1}</a>";

        /// <summary>
        ///     br tag
        /// </summary>
        internal const string LineBreak = "<br>";

        /// <summary>
        ///     [h1 style='{0}' title='{1}']{2}[/h1]
        /// </summary>
        internal const string H1 = "<h1 style='{0}' title='{1}'>{2}</h1>";

        /// <summary>
        ///     [h2 style='{0}' title='{1}']{2}[/h2]
        /// </summary>
        internal const string H2 = "<h2 style='{0}' title='{1}'>{2}</h2>";

        /// <summary>
        ///     [h3 style='{0}' title='{1}']{2}[/h3]
        /// </summary>
        internal const string H3 = "<h3 style='{0}' title='{1}'>{2}</h3>";

        /// <summary>
        ///     [h4 style='{0}' title='{1}']{2}[/h4]
        /// </summary>
        internal const string H4 = "<h4 style='{0}' title='{1}'>{2}</h4>";

        /// <summary>
        ///     [div style='{0}' title='{1}']{2}[/div]
        /// </summary>
        internal const string DivTag = "<div style='{0}' title='{1}'>{2}</div>";

        /// <summary>
        ///     [span style='{0}' title='{1}']{2}[/span]
        /// </summary>
        internal const string SpanTag = "<span style='{0}' title='{1}'>{2}</span>";

        /// <summary>
        ///     [strong style='{0}' title='{1}']{2}[/strong]
        /// </summary>
        internal const string StrongTag = "<strong style='{0}' title='{1}'>{2}</strong>";

        #endregion
    }
}