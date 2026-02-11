using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLogger.Models
{
    public class Review
    {
        public string ReviewContent { get; set; } = string.Empty;
        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }

        public Review() { }
        public Review(string reviewContent, int reviewId, int gameId, int rating, DateTime datePosted)
        {
            this.ReviewContent = reviewContent;
            this.ReviewId = reviewId;
            this.GameId = gameId;
            this.Rating = rating;
            this.DatePosted = datePosted;
        }
    }
}
