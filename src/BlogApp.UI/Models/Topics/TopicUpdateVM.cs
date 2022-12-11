using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.UI.Models.Topics;

public class TopicUpdateVM
{
    public Guid Id { get; set; }

    [Required]
    [MinLength(2)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Thumbnail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile? ThumbnailFile { get; set; } = null!;

    public string Thumbnail { get; set; } = null!;
}
