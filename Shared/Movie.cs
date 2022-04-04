using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMovies.Shared
{
    public class Movie
    {
        [Key]
        public string ImdbId { get; set; }
        public string? EngName { get; set; }
        public string? RuName { get; set; }
        public HashSet<Actor> Actors { get; set; }
        public HashSet<Director> Directors { get; set; }
        public HashSet<Tag> Tags { get; set; }
        public double Rating { get; set; }
        public string TenSimilarMovies { get; set; }
        [NotMapped]
        public string Plot { get; set; }
        [NotMapped]
        public string Poster { get; set; }
    }
}
