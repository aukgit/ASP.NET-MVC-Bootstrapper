using System.Security.Principal;

namespace Microsoft.AspNet.Identity {
    public static class ExtentsionUserIdentityMethods {
        public static long GetUserId(this IIdentity identity) {
            return long.Parse(IdentityExtensions.GetUserId(identity));
        }
    }
}