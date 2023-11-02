namespace HackerNews.Models.Stories;

public class Story
{
     public string By { get; set; }

     public long Descendants { get; set; }

     public long Id { get; set; }

     public int Score { get; set; }

     public long Time { get; set; }

     public string Title { get; set; }

     public string Url { get; set; }
}
