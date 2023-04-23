using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.Infrastructure.Utilities.Common;
using DrShop2City.Infrastructure.Utilities.Extensions.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DrShop2City.WebApi.Identity
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _role;
        private IAccessService _accessService;

        public PermissionCheckerAttribute(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _accessService = (IAccessService)context.HttpContext.RequestServices.GetService(typeof(IAccessService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.GetUserId();

                var result = _accessService.CheckUserRole(userId, _role).Result;

                if (!result)//کاربر دسترسی ندارد
                {
                    context.Result = JsonResponseStatus.NoAccess();
                }
            }
            else
            {
                context.Result = JsonResponseStatus.NoAccess();
            }
        }
    }
}
