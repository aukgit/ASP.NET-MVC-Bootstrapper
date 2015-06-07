namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class CountryTimezoneRelation {
        public int CountryTimezoneRelationId { get; set; }
        public int UserTimeZoneId { get; set; }
        public int CountryId { get; set; }
    }
}