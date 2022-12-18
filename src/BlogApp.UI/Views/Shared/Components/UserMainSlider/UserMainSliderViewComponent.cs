using BlogApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Views.Shared.Components.UserMainSlider;

public class UserMainSliderViewComponent : ViewComponent
{
    private readonly IArticleService _articleService;
    public UserMainSliderViewComponent(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await _articleService.GetAllShortDetailsRandomlyAsync();
        return View(result?.Data);
    }
}
