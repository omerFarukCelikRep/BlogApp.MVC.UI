﻿using BlogApp.UI.Models.Articles;
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
            return RedirectToAction(nameof(Unpublished));
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
            ModelState.AddModelError(string.Empty, result.Message!);
            return View(articleAddVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Unpublished()
    {
        var result = await _articleService.GetAllUnpublished();
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!);
            return RedirectToAction(nameof(Index));
        }

        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> UnpublishedDetails(Guid id)
    {
        var result = await _articleService.GetUnpublishedById(id);
        if (!result!.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!);
            return RedirectToAction(nameof(Unpublished));
        }

        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Publish(Guid id)
    {
        var result = await _articleService.Publish(id);

        return RedirectToAction(result.IsSuccess ? nameof(Index) : nameof(Unpublished));
    }

    [HttpGet]
    public async Task<IActionResult> PublishedDetails(Guid id)
    {
        var result = await _articleService.GetPublishedById(id);

        return View(result?.Data);
    }

    private async Task<IEnumerable<SelectListItem>> GetTopics(Guid? selectedId = null)
    {
        var result = await _topicService.GetAll();
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