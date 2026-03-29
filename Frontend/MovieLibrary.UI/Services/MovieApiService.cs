using MovieLibrary.Domain.Entities;
using System.Net.Http.Headers;

namespace MovieLibrary.UI.Services;

public class MovieApiService : IMovieApiService
{
    private readonly HttpClient _http;
    private readonly ITokenProvider _tokenProvider;

    public MovieApiService(HttpClient http, ITokenProvider tokenProvider)
    {
        _http = http;
        _tokenProvider = tokenProvider;
    }

    // Attach the token managed by the token provider to each API request.
    private async Task SetAuthorizationHeaderAsync()
    {
        var token = await _tokenProvider.GetAccessTokenAsync();
        _http.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(token)
            ? null
            : new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        await SetAuthorizationHeaderAsync();
        return await _http.GetFromJsonAsync<IEnumerable<Movie>>("movies")
               ?? Enumerable.Empty<Movie>();
    }

    public async Task<Movie?> GetMovieAsync(int id)
    {
        await SetAuthorizationHeaderAsync();
        return await _http.GetFromJsonAsync<Movie>($"movies/{id}");
    }

    public async Task AddMovieAsync(Movie movie)
    {
        await SetAuthorizationHeaderAsync();
        var response = await _http.PostAsJsonAsync("movies", movie);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteMovieAsync(int id)
    {
        await SetAuthorizationHeaderAsync();
        var response = await _http.DeleteAsync($"movies/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<Genre>> GetGenresAsync()
    {
        await SetAuthorizationHeaderAsync();
        return await _http.GetFromJsonAsync<IEnumerable<Genre>>("genres")
               ?? Enumerable.Empty<Genre>();
    }

    public async Task AddGenreAsync(Genre genre)
    {
        await SetAuthorizationHeaderAsync();
        var response = await _http.PostAsJsonAsync("genres", genre);
        response.EnsureSuccessStatusCode();
    }
}
