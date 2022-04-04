using BlazorMovies.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace BlazorMovies.Client.Services
{
    public class MovieService: IMovieService
    {
        private readonly HttpClient _httpClient;
        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<Actor> Actors { get; set; } = new List<Actor>();
        public List<Director> Directors { get; set; } = new List<Director>();
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public event Action OnChange;

        public async Task<Actor> GetActor(string id)
        {
            using var httpResponse = await _httpClient.GetAsync($"api/actors/{id}", HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();
            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<Actor>(jsonReader);
        }

        public async Task<Director> GetDirector(string id)
        {
            using var httpResponse = await _httpClient.GetAsync($"api/directors/{id}", HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();
            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<Director>(jsonReader);
        }

        public async Task<Movie> GetMovie(string id)
        {
            using var httpResponse = await _httpClient.GetAsync($"api/movies/{id}", HttpCompletionOption.ResponseHeadersRead);

            httpResponse.EnsureSuccessStatusCode();
            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<Movie>(jsonReader);
        }
        public async Task<List<Movie>> GetMoviesByName(string name)
        {
            using var httpResponse = await _httpClient.GetAsync($"api/movies/search/{name}", HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();
            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<List<Movie>>(jsonReader);
        }

        public async Task<List<Movie>> GetSimilarMovies(Movie movie)
        {
            List<Movie> similarMovies = new List<Movie>();
            foreach (string id in movie.TenSimilarMovies.Split(','))
            {
                using var httpResponse = await _httpClient.GetAsync($"api/movies/{id}", HttpCompletionOption.ResponseHeadersRead);
                httpResponse.EnsureSuccessStatusCode();
                var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);
                JsonSerializer serializer = new JsonSerializer();
                similarMovies.Add(serializer.Deserialize<Movie>(jsonReader));
            }
            return similarMovies;
        }

        public async Task<List<Movie>> GetMovies()
        {
            Movies = await _httpClient.GetFromJsonAsync<List<Movie>>("api/movies");
            return Movies;
        }

        public async Task<Tag> GetTag(string id)
        {
            using var httpResponse = await _httpClient.GetAsync($"api/tags/{id}", HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();
            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<Tag>(jsonReader);
        }

        public async Task<List<Actor>> GetActorsByName(string name)
        {
            using var httpResponse = await _httpClient.GetAsync($"api/actors/search/{name}", HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();
            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<List<Actor>>(jsonReader);
        }

        public async Task<List<Director>> GetDirectorsByName(string name)
        {
            using var httpResponse = await _httpClient.GetAsync($"api/directors/search/{name}", HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();
            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<List<Director>>(jsonReader);
        }
        public async Task<List<Tag>> GetTagsByName(string name)
        {
            using var httpResponse = await _httpClient.GetAsync($"api/tags/search/{name}", HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();
            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<List<Tag>>(jsonReader);
        }
    }
}
