#region using block

using System;

#endregion

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class RegisterCodeUserRelation {
        public Guid RegisterCodeUserRelationId { get; set; }
        public long UserId { get; set; }
    }
}