using BlogApp.UI.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.UI.Models.Articles;

public class ArticleUnpublishedUpdateVM
{
    public Guid Id { get; set; }

    [Required]
    [MinLength(0)]
    public string Title { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;
    public string? Thumbnail => ThumbnailFile?.FileToStringAsync().GetAwaiter().GetResult();

    [Display(Name = "Thumbnail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile? ThumbnailFile { get; set; }

    [Required]
    [Display(Name = "Topics")]
    public List<Guid> TopicIds { get; set; } = null!;
    public IEnumerable<string> Topics { get; set; } = null!;
}
