namespace BlogApp.UI.Models.Topics;

public class TopicListVM
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Thumbnail { get; set; } = null!;
}
