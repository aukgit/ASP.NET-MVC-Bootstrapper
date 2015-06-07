#region using block

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class CountryCurrency {
        [Key]
        public int CountryCurrencyId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(7)]
        [Required]
        public string CurrencyName { get; set; }

        public int CountryId { get; set; }
    }
}