﻿using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace DevBootstrapper.Modules.Extensions.IdentityExtension {
    public static class ExtentsionUserIdentityMethods {
        public static long GetUserId(this IIdentity identity) {
            return long.Parse(IdentityExtensions.GetUserId(identity));
        }
    }
}