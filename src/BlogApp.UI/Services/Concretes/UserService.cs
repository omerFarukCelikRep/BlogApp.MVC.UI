using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.Articles;
using BlogApp.UI.Services.Interfaces;

namespace BlogApp.UI.Services.Concretes;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IDataResult<ArticleAuthorInfoVM>?> GetArticleUserInfo(Guid userId)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<ArticleAuthorInfoVM>>($"/api/v1/Users/GetUserInfo?userId={userId}");
    }
}
