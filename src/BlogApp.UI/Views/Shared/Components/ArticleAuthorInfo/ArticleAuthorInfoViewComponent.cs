using BlogApp.UI.Models.Articles;
using BlogApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Views.Shared.Components.ArticleAuthorInfo;

public class ArticleAuthorInfoViewComponent : ViewComponent
{
    private readonly IUserService _userService;
    public ArticleAuthorInfoViewComponent(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid userId)
    {
        var result = await _userService.GetArticleUserInfo(userId);
        return View(result!.IsSuccess ? result.Data : new ArticleAuthorInfoVM());
    }
}
