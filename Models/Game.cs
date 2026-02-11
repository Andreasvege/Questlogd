using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLogger.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }

        public Game() { }
        public Game(int id, string name, string genre, string developer, DateTime releaseDate)
        {
            this.Id = id;
            this.Name = name;
            this.Genre = genre;
            this.Developer = developer;
            this.ReleaseDate = releaseDate;
        }
        public override string ToString()
        {
            return $"{Name} ({Genre}) by {Developer}, released on {ReleaseDate.ToShortDateString()}";
        }
    }
} 
