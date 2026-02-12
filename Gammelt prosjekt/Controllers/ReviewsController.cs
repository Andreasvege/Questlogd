using Microsoft.AspNetCore.Mvc;
using Questlogd.Models;

namespace Questlogd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        // Her kan du legge til metoder for å håndtere HTTP-forespørsler relatert til anmeldelser
        private static List<Review> reviews = new List<Review>
        {
            new Review("Incredible sense of exploration and freedom.", 1, 1, 10, DateTime.UtcNow.AddDays(-10)),
            new Review("Combat and storytelling keep me coming back.", 2, 2, 9, DateTime.UtcNow.AddDays(-3))
        };

        [HttpGet]
        public IActionResult GetReviews()
        {
            return Ok(reviews);
        }

        [HttpGet("{gameId:int}")]
        public IActionResult GetReviewsByGameId(int gameId)
        {
            var gameReviews = reviews.Where(r => r.GameId == gameId);
            return Ok(gameReviews);
        }
        [HttpPost("{gameId:int}")]
        public IActionResult AddReview(int gameId, [FromBody] Review newReview)
        {
            if (newReview.Rating < 1 || newReview.Rating > 10)
            {
                return BadRequest("Rating must be between 1 and 10");
            }

            newReview.GameId = gameId;
            reviews.Add(newReview);
            return CreatedAtAction(nameof(GetReviewsByGameId), new { gameId = gameId }, newReview);
        }
    }
}