using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevBootstrapper.Models.POCO.Identity;

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class UserTimeZone {
        [Key]
        public int UserTimeZoneID { get; set; }

        /// <summary>
        ///     Windows TimeInfo ID
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [Required]
        [StringLength(50)]
        public string InfoID { get; set; }
        /// <summary>
        /// (UTC-07:00) Chihuahua, La Paz, Mazatlan
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [Required]
        [StringLength(70)]

        public string Display { get; set; }

        /// <summary>
        /// UTC-10:00
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [Required]
        [StringLength(10)]
        public string UTCName { get; set; }

        /// <summary>
        ///     -9
        /// </summary>
        public float UTCValue { get; set; }
        /// <summary>
        /// -07:00
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [Required]

        public string TimePartOnly { get; set; }

        [ForeignKey("UserTimeZoneID")]
        public ICollection<CountryTimezoneRelation> CountryTimezoneRelations { get; set; }

        [ForeignKey("UserTimeZoneID")]
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}