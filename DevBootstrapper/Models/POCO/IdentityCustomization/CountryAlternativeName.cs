#region using block

using System.ComponentModel.DataAnnotations;

#endregion

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class CountryAlternativeName {
        [Key]
        public int CountryAlternativeNameId { get; set; }

        [StringLength(80)]
        [Required]
        public string AlternativeName { get; set; }

        public int CountryId { get; set; }
    }
}