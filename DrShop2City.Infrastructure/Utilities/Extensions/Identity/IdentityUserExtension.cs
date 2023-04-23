using System;
using System.Security.Claims;

namespace DrShop2City.Infrastructure.Utilities.Extensions.Identity
{
    public static class IdentityUserExtension
    {
        public static long GetUserId(this ClaimsPrincipal? claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var result = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                return Convert.ToInt64(result.Value);
            }

            return default(long);

        }
    }
}
