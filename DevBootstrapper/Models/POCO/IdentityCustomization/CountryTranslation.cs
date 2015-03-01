using System.ComponentModel.DataAnnotations;

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class CountryTranslation {
        public int CountryTranslationId { get; set; }
        public int CountryLanguageId { get; set; }

        [StringLength(50)]
        [Required]
        public string Translation { get; set; }

        public int CountryId { get; set; }
    }
}