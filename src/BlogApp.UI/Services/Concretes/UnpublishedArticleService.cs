using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.UnpublishedArticles;
using BlogApp.UI.Services.Interfaces;

namespace BlogApp.UI.Services.Concretes;

public class UnpublishedArticleService : IUnpublishedArticleService
{
    private readonly HttpClient _httpClient;
    public UnpublishedArticleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IDataResult<List<ArticleUnpublishedListVM>>?> GetAllUnpublished()
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<ArticleUnpublishedListVM>>>("/api/v1/Articles/Unpublished");
    }
}
