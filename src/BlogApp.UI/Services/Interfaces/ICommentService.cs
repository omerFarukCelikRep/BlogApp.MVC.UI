using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.UI.Models.Articles;
using BlogApp.UI.Models.Comments;

namespace BlogApp.UI.Services.Interfaces;

public interface ICommentService
{
    Task<Core.Utilities.Results.Interfaces.IResult> AddAsync(CommentAddVM commentAddVM);
    Task<IDataResult<List<ArticleCommentListVM>>?> GetAllByArticleId(Guid articleId);
}
