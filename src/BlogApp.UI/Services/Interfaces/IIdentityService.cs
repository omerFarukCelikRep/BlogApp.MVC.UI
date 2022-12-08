using BlogApp.UI.Models.Authentication;
using IResult = BlogApp.Core.Utilities.Results.Interfaces.IResult;

namespace BlogApp.UI.Services.Interfaces;

public interface IIdentityService
{
    bool IsLoggedIn { get; }

    string? GetUserToken();
    Task<IResult> LoginAsync(LoginVM loginVM);
    Task<IResult> RegisterAsync(RegisterVM registerVM);
    Task SignOutAsync();
}
