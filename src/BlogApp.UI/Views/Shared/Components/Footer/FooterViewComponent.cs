using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Views.Shared.Components.Footer;

public class FooterViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View());
    }
}