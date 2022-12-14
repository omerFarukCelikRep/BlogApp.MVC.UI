using BlogApp.UI.Filters;
using BlogApp.UI.Handlers.Authentication;
using BlogApp.UI.Services.Concretes;
using BlogApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlogApp.UI.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMVCServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSession();

        //services.AddAuthentication("Basic")
        //.AddScheme<BlogAppAuthenticationSchemeOptions, BlogAppAuthenticationHandler>("Basic", null);

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.AccessDeniedPath = "/AccessDenied";
                options.LogoutPath = "/Logout";
                options.LoginPath = "/Login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.SlidingExpiration = true;
            });

        services.AddScoped<AuthTokenHandler>();

        services.AddHttpClient("WebApiClient", client =>
        {
            client.BaseAddress = new Uri(configuration["WebApiClient:Url"]!);
        })
        .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddHttpContextAccessor();

        services.AddScoped(serviceProvider =>
        {
            var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            return clientFactory.CreateClient("WebApiClient");
        });
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IUnpublishedArticleService, UnpublishedArticleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICommentService, CommentService>();

        //services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

        services.AddControllersWithViews(options => options.Filters.Add<CustomExceptionFilter>());

        return services;
    }
}
