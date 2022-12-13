using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApp.UI.Models.Topics;

public class TopicUpdateVM
{
    public Guid Id { get; set; }

    [Required]
    [MinLength(3)]
    [StringLength(256)]
    public string Name { get; set; } = null!;

    [MinLength(11)]
    [StringLength(512)]
    public string Description { get; set; } = null!;

    [Display(Name = "Thumbnail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IFormFile? ThumbnailFile { get; set; } = null!;

    [HiddenInput]
    public string Thumbnail { get; set; } = null!;
}
