using MovieLibrary.Domain.Entities;
using System.Net.Http.Headers;

namespace MovieLibrary.UI.Services;

public class MovieApiService : IMovieApiService
{
    private readonly HttpClient _http;

    public MovieApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<Movie>>("movies")
               ?? Enumerable.Empty<Movie>();
    }

    public async Task<Movie?> GetMovieAsync(int id)
    {
        return await _http.GetFromJsonAsync<Movie>($"movies/{id}");
    }

    public async Task AddMovieAsync(Movie movie)
    {
        var response = await _http.PostAsJsonAsync("movies", movie);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteMovieAsync(int id)
    {
        var response = await _http.DeleteAsync($"movies/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<Genre>> GetGenresAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<Genre>>("genres")
               ?? Enumerable.Empty<Genre>();
    }

    public async Task AddGenreAsync(Genre genre)
    {
        var response = await _http.PostAsJsonAsync("genres", genre);
        response.EnsureSuccessStatusCode();
    }
}
