using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.UnpublishedArticles;

namespace BlogApp.UI.Services.Interfaces;

public interface IUnpublishedArticleService
{
    Task<IDataResult<List<ArticleUnpublishedListVM>>?> GetAllAsync();
    Task<IDataResult<ArticleUnpublishedDetailsVM>?> GetByIdAsync(Guid articleId);
    Task<Core.Utilities.Results.Interfaces.IResult> PublishAsync(Guid articleId);
}
