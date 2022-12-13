using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Extensions;
using BlogApp.UI.Models.Topics;
using BlogApp.UI.Services.Interfaces;
using IResult = BlogApp.Core.Utilities.Results.Interfaces.IResult;

namespace BlogApp.UI.Services.Concretes;

public class TopicService : ITopicService
{
    private readonly HttpClient _httpClient;

    public TopicService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IDataResult<List<TopicListVM>>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<TopicListVM>>>("/api/v1/Topics", cancellationToken);
    }

    public async Task<IResult> AddAsync(TopicAddVM topicAddVM, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/v1/Topics", topicAddVM, cancellationToken);
        if (response is null)
        {
            return new ErrorResult("Giriş Başarısız"); //TODO: Magic string
        }

        var result = await response.Content.ReadFromJsonAsync<DataResult<TopicAddVM>>(cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return new ErrorResult($"{result!.Message}");
        }

        return new SuccessResult();
    }

    public async Task<IDataResult<TopicUpdateVM>?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<TopicUpdateVM>>("/api/v1/Topics/" + id, cancellationToken);
    }

    public async Task<IResult> UpdateAsync(TopicUpdateVM topicUpdateVM, CancellationToken cancellationToken = default)
    {
        if (topicUpdateVM.ThumbnailFile is not null)
        {
            topicUpdateVM.Thumbnail = await topicUpdateVM.ThumbnailFile.FileToStringAsync(cancellationToken);
        }

        var response = await _httpClient.PutAsJsonAsync("/api/v1/Topics", topicUpdateVM, cancellationToken);
        if (response is null || !response.IsSuccessStatusCode)
        {
            return new ErrorResult("İşlem Başarısız!");
        }

        var result = await response.Content.ReadFromJsonAsync<DataResult<TopicUpdateVM>>(cancellationToken: cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return new ErrorResult($"{result?.Message}");
        }

        return new SuccessResult();
    }

    public async Task<IResult?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _httpClient.DeleteFromJsonAsync<Result>("/api/v1/Topics/" + id, cancellationToken);
    }
}
