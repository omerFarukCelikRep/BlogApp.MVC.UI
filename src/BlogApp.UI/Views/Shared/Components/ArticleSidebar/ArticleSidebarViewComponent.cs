using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Views.Shared.Components.ArticleSidebar;

public class ArticleSidebarViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View());
    }
}
