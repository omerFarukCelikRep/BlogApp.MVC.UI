using BlogApp.UI.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.UI.Models.Articles;

public class ArticleAddVM
{
    [Required]
    [MinLength(0)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;
    public string? Thumbnail => ThumbnailFile?.FileToStringAsync().GetAwaiter().GetResult();

    [Display(Name = "Thumbnail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile? ThumbnailFile { get; set; }

    [Required]
    [Display(Name = "Topics")]
    public List<Guid> TopicIds { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IEnumerable<SelectListItem>? Topics { get; set; }
}