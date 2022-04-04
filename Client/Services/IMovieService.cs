using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMovies.Shared;

namespace BlazorMovies.Client.Services
{
    interface IMovieService
    {
        event Action OnChange;
        List<Movie> Movies { get; set; }
        List<Actor> Actors { get; set; }
        List<Director> Directors { get; set; }
        List<Tag> Tags { get; set; }
        Task<List<Movie>> GetMovies();
        Task<Movie> GetMovie(string id);
        Task<List<Movie>> GetMoviesByName(string name);
        Task<List<Actor>> GetActorsByName(string name);
        Task<List<Director>> GetDirectorsByName(string name);
        Task<List<Tag>> GetTagsByName(string name);
        Task<List<Movie>> GetSimilarMovies(Movie movie);
        Task<Actor> GetActor(string id);
        Task<Director> GetDirector(string id);
        Task<Tag> GetTag(string id);
    }
}
