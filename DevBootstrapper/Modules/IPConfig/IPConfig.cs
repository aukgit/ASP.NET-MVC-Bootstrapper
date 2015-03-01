using System;
using System.Net;
using System.Web;
using DevBootstrapper.Modules.Session;

namespace DevBootstrapper.Modules.IPConfig {
    public class IpConfig {
        public double IpToValue(string ipAddress) {
            double dot2LongIp = 0;
            var prevPos = 0;
            int pos;
            int num;

            for (var i = 1; i <= 4; i++) {
                pos = ipAddress.IndexOf(".", prevPos) + 1;
                if (i == 4) {
                    pos = ipAddress.Length + 1;
                }
                num = int.Parse(ipAddress.Substring(prevPos, pos - prevPos - 1));
                prevPos = pos;
                dot2LongIp = ((num % 256) * (256 ^ (4 - i))) + dot2LongIp;
            }
            return dot2LongIp;
        }

        /// <summary>
        ///     method to get Client ip address
        /// </summary>
        /// <param name="getLan"> set to true if want to get local(LAN) Connected ip address</param>
        /// <returns></returns>
        public static string GetVisitorIpAddress(bool getLan = false) {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionNames.IpAddress] != null) {
                return (string)HttpContext.Current.Session[SessionNames.IpAddress];
            }
            var visitorIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (String.IsNullOrEmpty(visitorIpAddress))
                visitorIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIpAddress))
                visitorIpAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIpAddress) || visitorIpAddress.Trim() == "::1") {
                getLan = true;
                visitorIpAddress = string.Empty;
            }

            if (getLan && string.IsNullOrEmpty(visitorIpAddress)) {
                //This is for Local(LAN) Connected ID Address
                var stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                var ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                var arrIpAddress = ipHostEntries.AddressList;

                try {
                    visitorIpAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                } catch {
                    try {
                        visitorIpAddress = arrIpAddress[0].ToString();
                    } catch {
                        try {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIpAddress = arrIpAddress[0].ToString();
                        } catch {
                            visitorIpAddress = "127.0.0.1";
                        }
                    }
                }
            }

            if (HttpContext.Current.Session != null) {
                HttpContext.Current.Session[SessionNames.IpAddress] = visitorIpAddress;
            }
            return visitorIpAddress;
        }

        public static string GetIpAddress() {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionNames.IpAddress] != null) {
                return (string)HttpContext.Current.Session[SessionNames.IpAddress];
            }
            var context = HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress)) {
                var addresses = ipAddress.Split(',');
                if (addresses.Length != 0) {
                    return addresses[0];
                }
            }
            var finalIp = HttpContext.Current.Request.UserHostAddress;
            if (HttpContext.Current.Session != null) {
                HttpContext.Current.Session[SessionNames.IpAddress] = finalIp;
            }
            return finalIp;
        }
    }
}