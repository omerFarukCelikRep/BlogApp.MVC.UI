using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.Articles;

namespace BlogApp.UI.Services.Interfaces;

public interface IUserService
{
    Task<IDataResult<ArticleAuthorInfoVM>?> GetArticleUserInfo(Guid userId);
}