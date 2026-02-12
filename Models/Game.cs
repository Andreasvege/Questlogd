namespace Questlogd.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Developer { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string CoverImageUrl { get; set; } = string.Empty;
    public GameStatus? Status { get; set; }
    public int? Rating { get; set; }
    public int? HoursPlayed { get; set; }
    public DateTime? LastPlayed { get; set; }
    public string? Review { get; set; }

    public Game() { }

    public Game(int id, string name, string genre, string developer, DateTime releaseDate,
                string coverImageUrl, GameStatus? status = null, int? rating = null,
                int? hoursPlayed = null, DateTime? lastPlayed = null, string? review = null)
    {
        Id = id;
        Name = name;
        Genre = genre;
        Developer = developer;
        ReleaseDate = releaseDate;
        CoverImageUrl = coverImageUrl;
        Status = status;
        Rating = rating;
        HoursPlayed = hoursPlayed;
        LastPlayed = lastPlayed;
        Review = review;
    }
}
