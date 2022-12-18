using BlogApp.UI.Extensions;
using BlogApp.UI.Models.Articles;
using BlogApp.UI.Services.Concretes;
using BlogApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        var result = await _articleService.GetAllAsync();

        return View(result?.Data);
    }

    [HttpGet]
    public async Task<IActionResult> ListByTopic([FromQuery(Name = "t")] string topicName)
    {
        var result = await _articleService.GetAllByTopicNameAsync(topicName);

        return View(nameof(Index), result?.Data);
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

        return RedirectToAction(nameof(Index),"UnpublishedArticle");
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _articleService.GetByIdAsync(id);
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!);
            return RedirectToAction(nameof(Index));
        }

        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Update([FromServices] IUnpublishedArticleService unpublishedArticleService,Guid id)
    {
        var result = await unpublishedArticleService.GetByIdAsync(id);
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!); //TODO:Show message
            return RedirectToAction(nameof(Index));
        }

        ArticleUpdateVM articleUpdateVM = (ArticleUpdateVM)result.Data!;
        articleUpdateVM.Topics = await GetTopics(articleUpdateVM.TopicIds);
        return View(articleUpdateVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ArticleUpdateVM articleUpdateVM)
    {
        if (!ModelState.IsValid)
        {
            articleUpdateVM.Topics = await GetTopics(articleUpdateVM.TopicIds);
            return View(articleUpdateVM);
        }

        if (articleUpdateVM.ThumbnailFile is not null)
        {
            articleUpdateVM.Thumbnail = await articleUpdateVM.ThumbnailFile.FileToStringAsync();
        }

        var result = await _articleService.UpdateAsync(articleUpdateVM);
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!); //TODO:Show message
            return View(articleUpdateVM);
        }

        return RedirectToAction(nameof(Details), new { id = articleUpdateVM.Id });
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
