using MovieLibrary.Domain.Entities;

namespace MovieLibrary.Application.Services;

public interface IMovieService
{
    IEnumerable<Movie> GetAllMovies();
    Movie? GetMovieById(int id);
    void AddMovie(Movie movie);
    bool UpdateMovie(int id, Movie movie);
    bool DeleteMovie(int id);
}
