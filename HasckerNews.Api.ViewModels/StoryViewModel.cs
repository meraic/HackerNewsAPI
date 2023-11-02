namespace HasckerNews.Api.ViewModels;

public class StoryViewModel
{
    public string Title { get; set; }

    public string Url { get; set; }

    public string PostedBy { get; set; }

    public long Time { get; set; }

    public int Score { get; set; }

    public long CommentCount { get; set; }
}