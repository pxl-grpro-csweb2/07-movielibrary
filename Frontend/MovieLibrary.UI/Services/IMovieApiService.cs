using MovieLibrary.Domain.Entities;

namespace MovieLibrary.UI.Services;

public interface IMovieApiService
{
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task<Movie?> GetMovieAsync(int id);
    Task AddMovieAsync(Movie movie);
    Task DeleteMovieAsync(int id);

    Task<IEnumerable<Genre>> GetGenresAsync();
    Task AddGenreAsync(Genre genre);
}
