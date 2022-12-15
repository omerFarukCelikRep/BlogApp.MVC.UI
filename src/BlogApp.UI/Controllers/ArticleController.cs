using BlogApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Controllers;
public class ArticleController : BaseController
{
    private readonly ITopicService _topicService;
    private readonly IArticleService _articleService;
    public ArticleController(ITopicService topicService, IArticleService articleService)
    {
        _topicService = topicService;
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _articleService.GetAllPublished();

        return View(result?.Data);
    }

    [HttpGet]
    public async Task<IActionResult> ListByTopic([FromQuery(Name = "t")] string topicName)
    {
        var result = await _articleService.GetAllPublishedByTopicName(topicName);

        return View(nameof(Index), result?.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _articleService.GetPublishedById(id);
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!);
            return RedirectToAction(nameof(Index));
        }

        return View(result.Data);
    }
}
