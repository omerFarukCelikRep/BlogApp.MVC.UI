using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogApp.UI.Middlewares;

public class UserClaimsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserClaimsMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task InvokeAsync(HttpContext httpContext)
    {
        var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
        if (!string.IsNullOrEmpty(token))
        {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            httpContext.User = new(new ClaimsIdentity(jwtSecurityToken.Claims, "JwtAuthType"));
        }

        return _next(httpContext);
    }
}
