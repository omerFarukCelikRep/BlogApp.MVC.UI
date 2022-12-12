using BlogApp.UI.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.UI.Models.Topics;

public class TopicAddVM
{
    [Required]
    [MinLength(3)]
    [StringLength(256)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Thumbnail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile ThumbnailFile { get; set; } = null!;

    [Required]
    [MinLength(11)]
    [StringLength(512)]
    public string Description { get; set; } = string.Empty;

    public string? Thumbnail => ThumbnailFile?.FileToStringAsync().GetAwaiter().GetResult();
}
