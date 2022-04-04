using System.Collections.Generic;

namespace BlazorMovies.Shared
{
    public class Actor: Person
    {
        public HashSet<Movie> Movies { get; set; } = new HashSet<Movie>();
        public Actor(string personId, string name) : base(personId, name) { }

    }
}