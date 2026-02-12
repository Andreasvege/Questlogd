using Microsoft.AspNetCore.Mvc;
using Questlogd.Models;

namespace Questlogd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        //Legger inn litt sample data
        private static List<Game> games = new List<Game>
        {
            new(1, "Batman Arkham Knight", "Action-Adventure", "Rocksteady Studios", new DateTime(2015, 6, 23)),
            new(2, "The Witcher 3: Wild Hunt", "RPG", "CD Projekt Red", new DateTime(2015, 5, 19)),
            new(3, "Minecraft", "Sandbox", "Mojang Studios", new DateTime(2011, 11, 18)),
            new(4, "Cyberpunk 2077", "RPG", "CD Projekt Red", new DateTime(2020, 12, 10)),
            new(5, "God of War", "Action-Adventure", "Santa Monica Studio", new DateTime(2018, 4, 20)),
            new(6, "Red Dead Redemption 2", "Action-Adventure", "Rockstar Games", new DateTime(2018, 10, 26)),
            new(7, "Hollow Knight", "Metroidvania", "Team Cherry", new DateTime(2017, 2, 24)),
            new(8, "Hollow Knight: Silksong", "Metroidvania", "Team Cherry", new DateTime(2025, 9, 4)),
            new(9, "Elden Ring", "Action RPG", "FromSoftware", new DateTime(2022, 2, 25)),
            new(10, "Hogwarts Legacy", "Action RPG", "Portkey Games", new DateTime(2023, 2, 10)),
            new(11, "The Last of Us Part I", "Action-Adventure", "Naughty Dog", new DateTime(2022, 9, 2)),
            new(12, "The Last of Us Part II", "Action-Adventure", "Naughty Dog", new DateTime(2020, 6, 19)),
            new(13, "Hades", "Roguelike", "Supergiant Games", new DateTime(2020, 9, 17))
        };

        [HttpGet]
        public IActionResult GetGames()
        {
            return Ok(games);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetGameById(int id)
        {
            var game = games.SingleOrDefault(g => g.Id == id);
            return game is null ? NotFound() : Ok(game);
        }

        [HttpPost]
        public IActionResult AddGame([FromBody] Game newGame)
        {
            if (string.IsNullOrWhiteSpace(newGame.Name))
            {
                return BadRequest("Game name is required.");
            }

            var newId = games.Count == 0 ? 1 : games.Max(g => g.Id) + 1;
            newGame.Id = newId;
            games.Add(newGame);
            return CreatedAtAction(nameof(GetGameById), new { id = newGame.Id }, newGame);
        }

        [HttpGet("/games/view")]
        public IActionResult Index()
        {
            return View("Index", games);
        }


        [HttpGet("/games/view/{id:int}")]
        public IActionResult ViewGame(int id)
        {
            var game = games.SingleOrDefault(g => g.Id == id);
            return game is null ? NotFound() : View("Details", new List<Game> { game });
        }
        [HttpGet("/games/view/create")]
        public IActionResult CreateView()
        {
            return View("Create", games);
        }
        [HttpPost("/games/view/create")]
        public IActionResult CreateView(Game newGame)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", new List<Game> { newGame });
            }
            newGame.Id = games.Max(g => g.Id) + 1;
            games.Add(newGame);
            return RedirectToAction("ViewGames");
        }
    }
}