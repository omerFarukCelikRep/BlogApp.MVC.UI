using BlogApp.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Views.Shared.Components.FooterTrendTopics;

public class FooterTrendTopicsViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        await Task.CompletedTask;

        return View(Enumerable.Empty<FooterTopicListVM>());
    }
}
