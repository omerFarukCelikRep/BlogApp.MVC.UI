using BlogApp.UI.Models.Articles;
using BlogApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.UI.Controllers;
public class UnpublishedArticleController : BaseController
{
    private readonly IArticleService _articleService;
    private readonly ITopicService _topicService;
    public UnpublishedArticleController(IArticleService articleService, ITopicService topicService)
    {
        _articleService = articleService;
        _topicService = topicService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _articleService.GetAllUnpublished();
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!);
            return RedirectToAction("Index", "Home");
        }

        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View(new ArticleAddVM
        {
            Topics = await GetTopics()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add(ArticleAddVM articleAddVM)
    {
        if (!ModelState.IsValid)
        {
            articleAddVM.Topics = await GetTopics();
            return View(articleAddVM);
        }

        var result = await _articleService.AddAsync(articleAddVM);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!); //TODO:Show message
            return View(articleAddVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _articleService.GetUnpublishedById(id);
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!); //TODO:Show message
            return RedirectToAction(nameof(Index));
        }

        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Publish(Guid id)
    {
        var result = await _articleService.Publish(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!); //TODO:Show message
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction("Index", "Article");
    }

    private async Task<IEnumerable<SelectListItem>> GetTopics(Guid? selectedId = null)
    {
        var result = await _topicService.GetAllAsync();
        if (result is null)
        {
            return Enumerable.Empty<SelectListItem>();
        }

        return result.Data!.ConvertAll(topic => new SelectListItem
        {
            Selected = selectedId is not null && selectedId == topic.Id,
            Text = topic.Name,
            Value = topic.Id.ToString()
        });
    }
}
