using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class CountryDomain {
        public int CountryDomainId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(6)]
        public string Domain { get; set; }

        public int CountryId { get; set; }
    }
}