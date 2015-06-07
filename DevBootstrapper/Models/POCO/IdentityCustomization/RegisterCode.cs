#region using block

using System;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class RegisterCode {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RegisterCodeId { get; set; }

        public long RoleId { get; set; }
        public DateTime GeneratedDate { get; set; }
        public DateTime ValidityTill { get; set; }
        public bool IsUsed { get; set; }
        public bool IsExpired { get; set; }
    }
}