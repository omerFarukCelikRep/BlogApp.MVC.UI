using BlogApp.UI.Models.Comments;
using BlogApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Controllers;
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(CommentAddVM commentAddVM)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Details", "Article", new { id = commentAddVM.ArticleId });
        }

        var result = await _commentService.AddAsync(commentAddVM);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Message!);
        }

        return RedirectToAction("Details", "Article", new { id = commentAddVM.ArticleId });
    }
}
