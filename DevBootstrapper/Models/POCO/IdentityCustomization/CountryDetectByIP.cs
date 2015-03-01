using System.ComponentModel.DataAnnotations;

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class CountryDetectByIp {
        [Key]
        public int CountryDetectByIpid { get; set; }

        public int BeginingIp { get; set; }
        public int EndingIp { get; set; }
        public int CountryId { get; set; }
    }
}