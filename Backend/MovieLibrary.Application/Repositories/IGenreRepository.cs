using MovieLibrary.Domain.Entities;

namespace MovieLibrary.Application.Repositories;

public interface IGenreRepository
{
    IEnumerable<Genre> GetAll();
    Genre? GetById(int id);
    void Add(Genre genre);
    void SaveChanges();
}
