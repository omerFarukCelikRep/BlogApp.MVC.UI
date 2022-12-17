using BlogApp.UI.Models.Topics;

namespace BlogApp.UI.Models.UnpublishedArticles;

public class ArticleUnpublishedDetailsVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int ReadTime { get; set; }
    public string? Thumbnail { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<TopicArticleDetailsVM> Topics { get; set; } = null!;
    public string AuthorName { get; set; } = null!;
}
