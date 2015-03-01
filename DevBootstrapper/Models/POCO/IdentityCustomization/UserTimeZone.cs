using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevBootstrapper.Models.POCO.Identity;

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class UserTimeZone {
        [Key]
        public int UserTimeZoneId { get; set; }

        /// <summary>
        ///     Windows TimeInfo ID
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [Required]
        [StringLength(50)]
        public string InfoId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        [StringLength(70)]
        /// <summary>
        /// (UTC-07:00) Chihuahua, La Paz, Mazatlan
        /// </summary>
        public string Display { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        [StringLength(10)]

        /// <summary>
        /// UTC-10:00
        /// </summary>
        public string UtcName { get; set; }

        /// <summary>
        ///     -9
        /// </summary>
        public float UtcValue { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [Required]
        /// <summary>
        /// -07:00
        /// </summary>
        public string TimePartOnly { get; set; }

        [ForeignKey("UserTimeZoneID")]
        public ICollection<CountryTimezoneRelation> CountryTimezoneRelations { get; set; }

        [ForeignKey("UserTimeZoneID")]
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}