using BlazorMovies.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Services
{
    interface IOmdbService
    {
        Task GetMoviePlotAndPoster(Movie movie);
    }
}
