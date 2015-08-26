using System;
using DevBootstrapper.Models.POCO.IdentityCustomization;

namespace DevBootstrapper.Modules.TimeZone {
    public class TimeZoneSet {
        public TimeZoneInfo TimeZoneInfo { get; set; }
        public UserTimeZone UserTimezone { get; set; }


        public bool IsTimeZoneInfoExist() {
            return TimeZoneInfo != null;
        }

        public bool IsUserTimeZoneInfoExist() {
            return UserTimezone != null;
        }
    }
}