using BlogApp.UI.Extensions;
using BlogApp.UI.Models.Articles;
using BlogApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.UI.Controllers;
public class UnpublishedArticleController : BaseController
{
    private readonly IUnpublishedArticleService _unpublishedArticleService;
    private readonly IArticleService _articleService;
    private readonly ITopicService _topicService;
    public UnpublishedArticleController(IUnpublishedArticleService unpublishedArticleService, ITopicService topicService, IArticleService articleService)
    {
        _unpublishedArticleService = unpublishedArticleService;
        _topicService = topicService;
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _unpublishedArticleService.GetAllAsync();
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!);
            return RedirectToAction("Index", "Home");
        }

        return View(result.Data);
    }    

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _unpublishedArticleService.GetByIdAsync(id);
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!); //TODO:Show message
            return RedirectToAction(nameof(Index));
        }

        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Publish(Guid id)
    {
        var result = await _unpublishedArticleService.Publish(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!); //TODO:Show message
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction("Index", "Article");
    }

    private async Task<IEnumerable<SelectListItem>> GetTopics(List<Guid>? selectedIds = null)
    {
        var result = await _topicService.GetAllAsync();
        if (result is null)
        {
            return Enumerable.Empty<SelectListItem>();
        }

        return result.Data!.ConvertAll(topic => new SelectListItem
        {
            Selected = selectedIds is not null && selectedIds.Any(x => x == topic.Id),
            Text = topic.Name,
            Value = topic.Id.ToString()
        });
    }
}
