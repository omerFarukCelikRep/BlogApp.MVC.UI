using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Views.Shared.Components.Header;

public class HeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        await Task.CompletedTask;
        return View();
    }
}
