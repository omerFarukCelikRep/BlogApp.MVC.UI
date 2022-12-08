using BlogApp.UI.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Views.Shared.Components.ArticleAddComment;

public class ArticleAddCommentViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(Guid articleId)
    {
        return await Task.FromResult(View(new CommentAddVM()
        {
            ArticleId = articleId
        }));
    }
}
