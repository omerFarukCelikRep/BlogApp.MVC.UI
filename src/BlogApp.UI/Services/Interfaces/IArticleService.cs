using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.Articles;
using BlogApp.UI.Models.Users;

namespace BlogApp.UI.Services.Interfaces;

public interface IArticleService
{
    Task<Core.Utilities.Results.Interfaces.IResult> AddAsync(ArticleAddVM articleAddVM);
    Task<IDataResult<List<ArticlePublishedListVM>>?> GetAllAsync();
    Task<IDataResult<List<ArticlePublishedListVM>>?> GetAllByTopicNameAsync(string topicName);
    Task<IDataResult<List<UserMainSliderVM>>?> GetAllShortDetailsRandomlyAsync();
    Task<IDataResult<ArticleDetailsVM>?> GetByIdAsync(Guid articleId);
    Task<Core.Utilities.Results.Interfaces.IResult> UpdateAsync(ArticleUpdateVM articleUpdateVM);
}
