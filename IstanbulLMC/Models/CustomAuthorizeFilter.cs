using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace IstanbulLMC.Models
{

    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = new object[] { "admin" };
        }
    }
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly string _authenticationScheme;

        public CustomAuthorizeFilter(string authenticationScheme)
        {
            _authenticationScheme = authenticationScheme;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Admin", null);
            }
        }
    }
}
