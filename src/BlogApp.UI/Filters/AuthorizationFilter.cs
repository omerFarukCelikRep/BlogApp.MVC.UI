using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogApp.UI.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        bool isAnonymous = context.Filters.Any(x => x.GetType() == typeof(AllowAnonymousFilter));
        if (!context.HttpContext.User.Identity!.IsAuthenticated && !isAnonymous)
        {
            string path = context.HttpContext.Request.Path;
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = "Home", action = "Login", returnUrl = path })
            );
        }

        return Task.CompletedTask;
    }
}
