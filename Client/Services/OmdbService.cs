using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BlazorMovies.Shared;

namespace BlazorMovies.Client.Services
{
    public class OmdbService : IOmdbService
    {
        private readonly HttpClient _httpClient;
        public OmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        const string ApiKey = "7f743aba";
        public async Task GetMoviePlotAndPoster(Movie movie)
        {
            using var client = new HttpClient { BaseAddress = new Uri("https://www.omdbapi.com/?") };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client
                .GetAsync($"https://www.omdbapi.com/?apikey={ApiKey}&i={movie.ImdbId}&plot=full")
                .ConfigureAwait(false);
            var contentStream = await response.Content.ReadAsStringAsync();
            JsonSerializer serializer = new JsonSerializer();
            JObject json = JObject.Parse(contentStream);
            movie.Plot = json["Plot"].ToString();
            movie.Poster = json["Poster"].ToString();
        }
    }
}
