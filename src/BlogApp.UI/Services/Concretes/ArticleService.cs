﻿using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.Articles;
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

    public async Task<IDataResult<List<ArticlePublishedListVM>>?> GetAllPublished()
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<ArticlePublishedListVM>>>("/api/v1/Articles/Published");
    }

    public async Task<IDataResult<List<ArticlePublishedListVM>>?> GetAllPublishedByTopicName(string topicName)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<ArticlePublishedListVM>>>($"/api/v1/Articles/{topicName}");
    }

    public async Task<IDataResult<ArticlePublishedDetailsVM>?> GetPublishedById(Guid articleId)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<ArticlePublishedDetailsVM>>($"/api/v1/Articles/{articleId}");
    }

    public async Task<IDataResult<List<ArticleUnpublishedListVM>>?> GetAllUnpublished()
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<ArticleUnpublishedListVM>>>("/api/v1/Articles/Unpublished");
    }

    public async Task<IDataResult<ArticleUnpublishedDetailsVM>?> GetUnpublishedById(Guid articleId)
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

    public async Task<IDataResult<List<UserMainSliderVM>>?> GetAllPublishedShortDetailsRandomly()
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<UserMainSliderVM>>>($"/api/v1/Articles/ShortDetails");
    }
}