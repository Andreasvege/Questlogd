namespace Questlogd.Models;

public class Review
{
    public int ReviewId { get; set; }
    public int GameId { get; set; }
    public string ReviewContent { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime DatePosted { get; set; }

    public Review() { }

    public Review(int reviewId, int gameId, string reviewContent, int rating, DateTime datePosted)
    {
        ReviewId = reviewId;
        GameId = gameId;
        ReviewContent = reviewContent;
        Rating = rating;
        DatePosted = datePosted;
    }
}
