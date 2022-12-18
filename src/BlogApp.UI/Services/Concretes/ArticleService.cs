using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.Articles;
using BlogApp.UI.Models.UnpublishedArticles;
using BlogApp.UI.Models.Users;
using BlogApp.UI.Services.Interfaces;
using System.Net;
using IResult = BlogApp.Core.Utilities.Results.Interfaces.IResult;

namespace BlogApp.UI.Services.Concretes;

public class ArticleService : IArticleService
{
    private readonly HttpClient _httpClient;
    public ArticleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IResult> AddAsync(ArticleAddVM articleAddVM)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("/api/v1/Articles", articleAddVM);
        if (responseMessage is null)
        {
            return new ErrorResult("İşlem Başarısız"); //TODO: Magic string
        }

        var response = await responseMessage.Content.ReadFromJsonAsync<DataResult<ArticleAddVM>>();
        if (responseMessage.StatusCode == HttpStatusCode.BadRequest || !responseMessage.IsSuccessStatusCode)
        {
            return new ErrorResult($"{responseMessage.ReasonPhrase} - {response!.Message}");
        }

        return new SuccessResult();
    }

    public async Task<IDataResult<List<ArticlePublishedListVM>>?> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<ArticlePublishedListVM>>>("/api/v1/Articles/Published");
    }

    public async Task<IDataResult<List<ArticlePublishedListVM>>?> GetAllByTopicNameAsync(string topicName)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<ArticlePublishedListVM>>>($"/api/v1/Articles/{topicName}");
    }

    public async Task<IDataResult<ArticleDetailsVM>?> GetByIdAsync(Guid articleId)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<ArticleDetailsVM>>($"/api/v1/Articles/{articleId}");
    }

    public async Task<IDataResult<List<UserMainSliderVM>>?> GetAllShortDetailsRandomlyAsync()
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<UserMainSliderVM>>>($"/api/v1/Articles/ShortDetails");
    }

    public async Task<IResult> UpdateAsync(ArticleUpdateVM articleUpdateVM)
    {
        var responseMessage = await _httpClient.PutAsJsonAsync("/api/v1/Articles", articleUpdateVM);
        if (responseMessage is null)
        {
            return new ErrorResult("İşlem Başarısız"); //TODO: Magic string
        }

        var response = await responseMessage.Content.ReadFromJsonAsync<DataResult<ArticleUpdateVM>>();
        if (responseMessage.StatusCode == HttpStatusCode.BadRequest || !responseMessage.IsSuccessStatusCode)
        {
            return new ErrorResult($"{responseMessage.ReasonPhrase} - {response!.Message}");
        }

        return new SuccessResult();
    }
}