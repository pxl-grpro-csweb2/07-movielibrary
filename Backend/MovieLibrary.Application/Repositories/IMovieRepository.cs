using MovieLibrary.Domain.Entities;

namespace MovieLibrary.Application.Repositories;

public interface IMovieRepository
{
    IEnumerable<Movie> GetAll();
    Movie? GetById(int id);
    void Add(Movie movie);
    void Delete(Movie movie);
    void SaveChanges();
}
