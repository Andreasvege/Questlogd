using Questlogd.Models;

namespace Questlogd.Services;

public class GameService
{
    private readonly List<Game> _games;

    public GameService()
    {
        _games = InitializeSampleData();
    }

    // === GET METHODS ===

    public List<Game> GetAll() => _games;

    public Game? GetById(int id) => _games.FirstOrDefault(g => g.Id == id);

    public List<Game> GetByStatus(GameStatus status) =>
        _games.Where(g => g.Status == status).ToList();

    public List<Game> GetLogged() =>
        _games.Where(g => g.Status.HasValue).ToList();

    public List<Game> GetUnlogged() =>
        _games.Where(g => !g.Status.HasValue).ToList();

    public List<Game> GetPlaying() => GetByStatus(GameStatus.Playing);

    public List<Game> GetCompleted() => GetByStatus(GameStatus.Completed);

    public List<Game> GetBacklog() => GetByStatus(GameStatus.Backlog);

    public List<Game> GetWishlist() => GetByStatus(GameStatus.Wishlist);

    public List<Game> GetWithReviews() =>
        _games.Where(g => !string.IsNullOrEmpty(g.Review) || g.Rating.HasValue)
              .OrderByDescending(g => g.LastPlayed ?? g.ReleaseDate)
              .ToList();

    public List<Game> GetRecentlyPlayed(int count = 5) =>
        _games
            .Where(g => g.LastPlayed.HasValue)
            .OrderByDescending(g => g.LastPlayed)
            .Take(count)
            .ToList();

    // === SEARCH ===

    public List<Game> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return _games;

        var lowerQuery = query.ToLower();
        return _games
            .Where(g => g.Name.ToLower().Contains(lowerQuery) ||
                        g.Developer.ToLower().Contains(lowerQuery) ||
                        g.Genre.ToLower().Contains(lowerQuery))
            .ToList();
    }

    // === UPDATE METHODS ===

    public void UpdateGame(Game game)
    {
        var existing = _games.FirstOrDefault(g => g.Id == game.Id);
        if (existing != null)
        {
            var index = _games.IndexOf(existing);
            _games[index] = game;
        }
    }

    public void UpdateStatus(int gameId, GameStatus? newStatus)
    {
        var game = GetById(gameId);
        if (game != null)
        {
            game.Status = newStatus;
            if (newStatus == GameStatus.Playing)
            {
                game.LastPlayed = DateTime.Now;
            }
        }
    }

    public void LogGame(int gameId, GameStatus status, int? rating = null, string? review = null, int? hoursPlayed = null)
    {
        var game = GetById(gameId);
        if (game != null)
        {
            game.Status = status;
            game.Rating = rating;
            game.Review = review;
            game.HoursPlayed = hoursPlayed;
            game.LastPlayed = DateTime.Now;
        }
    }

    public void UpdateReview(int gameId, int? rating, string? review)
    {
        var game = GetById(gameId);
        if (game != null)
        {
            game.Rating = rating;
            game.Review = review;
        }
    }

    public void ClearStatus(int gameId)
    {
        var game = GetById(gameId);
        if (game != null)
        {
            game.Status = null;
        }
    }

    // === ADD METHODS ===

    public Game AddGame(string name, string genre, string developer, DateTime releaseDate, string coverImageUrl)
    {
        var newId = _games.Max(g => g.Id) + 1;
        var game = new Game(newId, name, genre, developer, releaseDate, coverImageUrl);
        _games.Add(game);
        return game;
    }

    // === SAMPLE DATA ===

    private List<Game> InitializeSampleData()
    {
        return new List<Game>
        {
            // Logged games (with status)
            new(1, "Elden Ring", "Action RPG", "FromSoftware", new DateTime(2022, 2, 25),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co4jni.webp",
                GameStatus.Playing, 10, 120, DateTime.Now.AddDays(-1),
                "Et mesterverk av åpen verden og utfordrende gameplay."),

            new(2, "The Witcher 3: Wild Hunt", "RPG", "CD Projekt Red", new DateTime(2015, 5, 19),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co1wyy.webp",
                GameStatus.Completed, 10, 180, DateTime.Now.AddMonths(-2),
                "En av de beste RPG-ene noensinne laget."),

            new(3, "Hollow Knight", "Metroidvania", "Team Cherry", new DateTime(2017, 2, 24),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co1rgi.webp",
                GameStatus.Playing, 9, 45, DateTime.Now.AddDays(-3)),

            new(4, "Cyberpunk 2077", "RPG", "CD Projekt Red", new DateTime(2020, 12, 10),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co4hk8.webp",
                GameStatus.Completed, 8, 95, DateTime.Now.AddMonths(-6)),

            new(5, "God of War Ragnarök", "Action-Adventure", "Santa Monica Studio", new DateTime(2022, 11, 9),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co5s5v.webp",
                GameStatus.Backlog),

            new(6, "Red Dead Redemption 2", "Action-Adventure", "Rockstar Games", new DateTime(2018, 10, 26),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co1q1f.webp",
                GameStatus.Backlog),

            new(7, "Hades", "Roguelike", "Supergiant Games", new DateTime(2020, 9, 17),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co1r0o.webp",
                GameStatus.Completed, 9, 65, DateTime.Now.AddMonths(-4)),

            new(8, "Baldur's Gate 3", "RPG", "Larian Studios", new DateTime(2023, 8, 3),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co670h.webp",
                GameStatus.Playing, null, 30, DateTime.Now.AddHours(-5)),

            new(9, "Hollow Knight: Silksong", "Metroidvania", "Team Cherry", new DateTime(2025, 9, 4),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co2tsc.webp",
                GameStatus.Wishlist),

            new(10, "Sekiro: Shadows Die Twice", "Action-Adventure", "FromSoftware", new DateTime(2019, 3, 22),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co1r77.webp",
                GameStatus.Backlog),

            new(11, "The Last of Us Part II", "Action-Adventure", "Naughty Dog", new DateTime(2020, 6, 19),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co5ziw.webp",
                GameStatus.Completed, 9, 28, DateTime.Now.AddMonths(-8)),

            new(12, "Ghost of Tsushima", "Action-Adventure", "Sucker Punch", new DateTime(2020, 7, 17),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co5tin.webp",
                GameStatus.Wishlist),

            new(13, "Celeste", "Platformer", "Maddy Makes Games", new DateTime(2018, 1, 25),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co3byy.webp",
                GameStatus.Completed, 10, 20, DateTime.Now.AddMonths(-12),
                "Perfekt balanse mellom vanskelighet og tilgjengelighet."),

            new(14, "Death Stranding", "Action", "Kojima Productions", new DateTime(2019, 11, 8),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co1xgn.webp",
                GameStatus.Dropped, 6, 15, DateTime.Now.AddMonths(-10)),

            new(15, "Disco Elysium", "RPG", "ZA/UM", new DateTime(2019, 10, 15),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co1sfj.webp",
                GameStatus.Backlog),

            // Unlogged games (no status - just in database)
            new(16, "Persona 5 Royal", "RPG", "Atlus", new DateTime(2019, 10, 31),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co2lck.webp"),

            new(17, "Dark Souls III", "Action RPG", "FromSoftware", new DateTime(2016, 4, 12),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co1vcf.webp"),

            new(18, "Horizon Forbidden West", "Action RPG", "Guerrilla Games", new DateTime(2022, 2, 18),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co4jeu.webp"),

            new(19, "Final Fantasy XVI", "Action RPG", "Square Enix", new DateTime(2023, 6, 22),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co5w3k.webp"),

            new(20, "Stardew Valley", "Simulation", "ConcernedApe", new DateTime(2016, 2, 26),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/xrpmydnu9rpxvxfjkiu7.webp"),

            new(21, "Monster Hunter: World", "Action RPG", "Capcom", new DateTime(2018, 1, 26),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co1wyy.webp"),

            new(22, "Returnal", "Roguelike", "Housemarque", new DateTime(2021, 4, 30),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co2g84.webp"),

            new(23, "It Takes Two", "Adventure", "Hazelight Studios", new DateTime(2021, 3, 26),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co2tvk.webp"),

            new(24, "Lies of P", "Action RPG", "Neowiz Games", new DateTime(2023, 9, 19),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co6bob.webp"),

            new(25, "Alan Wake 2", "Survival Horror", "Remedy Entertainment", new DateTime(2023, 10, 27),
                "https://images.igdb.com/igdb/image/upload/t_cover_big/co6gx4.webp")
        };
    }
}
