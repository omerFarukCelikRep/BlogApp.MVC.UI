using BlogApp.UI.Models.UnpublishedArticles;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.UI.Models.Articles;

public class ArticleUpdateVM
{
    public Guid Id { get; set; }

    [Required]
    [MinLength(0)]
    public string Title { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;
    public string? Thumbnail { get; set; }

    [Display(Name = "Thumbnail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile? ThumbnailFile { get; set; }

    [Required]
    [Display(Name = "Topics")]
    public List<Guid> TopicIds { get; set; } = null!;
    public IEnumerable<SelectListItem> Topics { get; set; } = null!;

    public static explicit operator ArticleUpdateVM(ArticleUnpublishedDetailsVM articleUnpublishedDetailsVM)
    {
        return new()
        {
            Id = articleUnpublishedDetailsVM.Id,
            Title = articleUnpublishedDetailsVM.Title,
            Content = articleUnpublishedDetailsVM.Content,
            Thumbnail = articleUnpublishedDetailsVM.Thumbnail,
            TopicIds = articleUnpublishedDetailsVM.Topics.Select(x => x.Id).ToList(),
        };
    }
}
