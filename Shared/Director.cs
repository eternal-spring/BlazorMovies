using System.Collections.Generic;

namespace BlazorMovies.Shared
{
    public class Director: Person
    {
        public HashSet<Movie> Movies { get; set; } = new HashSet<Movie>();
        public Director(string personId, string name) : base(personId, name) { }

    }
}