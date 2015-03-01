using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using DevBootstrapper.Models.DesignPattern.Interfaces;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DevBootstrapper.Models.POCO.Identity {
    public class ApplicationUser : IdentityUser<long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>,
        IDevUser {
        /// <summary>
        ///     Can't logged in if blocked by some admin.
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        ///     Reason for being blocked
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Blocking Reason")]
        [StringLength(20)]
        public string BlockingReason { get; set; }

        public long BlockedbyUserId { get; set; }
        //returns user Id
        public long UserId {
            get { return Id; }
        }

        #region Generate User

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #endregion

        #region Additional Properties with User

        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Last Name")]
        [StringLength(15)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string DisplayName {
            get { return FirstName + " " + LastName; }
        }

        [Required]
        [Display(Name = "Date of Birth")]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Registration Complete")]
        public bool IsRegistrationComplete { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "Country Language")]
        public int CountryLanguageId { get; set; }

        [Display(Name = "Timezone")]
        public int UserTimeZoneId { get; set; }


        [ForeignKey("UserID")]
        public virtual ICollection<RegisterCodeUserRelation> RegisterCodeUserRelations { get; set; }

        public Guid? GeneratedGuid { get; set; }

        #endregion
    }
}