using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorMovies.Shared
{
    public class Tag
    {
        [Key]
        public string TagId { get; set; }
        public string Name { get; set; }
        public HashSet<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}