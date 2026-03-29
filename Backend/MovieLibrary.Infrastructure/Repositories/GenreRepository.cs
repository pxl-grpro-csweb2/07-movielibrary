using MovieLibrary.Application.Repositories;
using MovieLibrary.Domain.Entities;
using MovieLibrary.Infrastructure.Data;

namespace MovieLibrary.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly MovieLibraryDbContext _context;

    public GenreRepository(MovieLibraryDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Genre> GetAll()
    {
        return _context.Genres.ToList();
    }

    public Genre? GetById(int id)
    {
        return _context.Genres.FirstOrDefault(g => g.Id == id);
    }

    public void Add(Genre genre)
    {
        _context.Genres.Add(genre);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
