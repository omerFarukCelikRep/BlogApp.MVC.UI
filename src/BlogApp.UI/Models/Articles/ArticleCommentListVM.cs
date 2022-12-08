namespace BlogApp.UI.Models.Articles;

public class ArticleCommentListVM
{
    public string UserName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string Text { get; set; } = string.Empty;
}
