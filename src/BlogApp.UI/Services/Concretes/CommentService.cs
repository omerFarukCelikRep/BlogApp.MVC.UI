using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.Articles;
using BlogApp.UI.Models.Comments;
using BlogApp.UI.Services.Interfaces;
using IResult = BlogApp.Core.Utilities.Results.Interfaces.IResult;

namespace BlogApp.UI.Services.Concretes;

public class CommentService : ICommentService
{
    private readonly HttpClient _httpClient;
    public CommentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IDataResult<List<ArticleCommentListVM>>?> GetAllByArticleId(Guid articleId)
    {
        return await _httpClient.GetFromJsonAsync<DataResult<List<ArticleCommentListVM>>>($"/api/v1/Comments/{articleId}");
    }

    public async Task<IResult> AddAsync(CommentAddVM commentAddVM)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("/api/v1/Comments", commentAddVM);
        if (responseMessage is null)
        {
            return new ErrorResult("İşlem Başarısız"); //TODO: Magic string
        }

        var response = await responseMessage.Content.ReadFromJsonAsync<DataResult<CommentAddVM>>();
        return !responseMessage.IsSuccessStatusCode
            ? new ErrorResult($"{responseMessage.ReasonPhrase} - {response?.Message}")
            : new SuccessResult();
    }
}
