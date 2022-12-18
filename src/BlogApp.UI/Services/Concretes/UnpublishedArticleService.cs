using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.UnpublishedArticles;
using BlogApp.UI.Services.Interfaces;
using IResult = BlogApp.Core.Utilities.Results.Interfaces.IResult;

namespace BlogApp.UI.Services.Concretes;

public class UnpublishedArticleService : IUnpublishedArticleService
{
    private readonly HttpClient _httpClient;
    public UnpublishedArticleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IDataResult<List<ArticleUnpublishedListVM>>?> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<ArticleUnpublishedListVM>>>("/api/v1/Articles/Unpublished");
    }

    public async Task<IDataResult<ArticleUnpublishedDetailsVM>?> GetByIdAsync(Guid articleId)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<ArticleUnpublishedDetailsVM>>($"/api/v1/Articles/Unpublished/{articleId}");
    }

    public async Task<IResult> Publish(Guid articleId)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("/api/v1/Articles/Publish", articleId);
        var response = await responseMessage.Content.ReadFromJsonAsync<Result>();
        if (response is not null && !responseMessage.IsSuccessStatusCode)
        {
            return new ErrorResult($"{responseMessage.ReasonPhrase} - {response.Message}");
        }

        return new SuccessResult();
    }
}
