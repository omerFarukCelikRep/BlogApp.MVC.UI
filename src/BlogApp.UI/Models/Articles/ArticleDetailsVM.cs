using BlogApp.UI.Models.Topics;

namespace BlogApp.UI.Models.Articles;

public class ArticleDetailsVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? Thumbnail { get; set; }
    public Guid UserId { get; set; }
    public string AuthorName { get; set; } = null!;
    public DateTime PublishDate { get; set; }
    public int CommentCount { get; set; }
    public int ReadTime { get; set; }
    public int ReadingCount { get; set; }
    public int LikeCount { get; set; }
    public List<TopicArticleDetailsVM> Topics { get; set; } = null!;
}
