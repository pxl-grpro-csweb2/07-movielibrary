using Microsoft.EntityFrameworkCore;
using MovieLibrary.Application.Repositories;
using MovieLibrary.Domain.Entities;
using MovieLibrary.Infrastructure.Data;

namespace MovieLibrary.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieLibraryDbContext _context;

    public MovieRepository(MovieLibraryDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Movie> GetAll()
    {
        return _context.Movies
            .Include(m => m.Genre)
            .ToList();
    }

    public Movie? GetById(int id)
    {
        return _context.Movies
            .Include(m => m.Genre)
            .FirstOrDefault(m => m.Id == id);
    }

    public void Add(Movie movie)
    {
        _context.Movies.Add(movie);
    }

    public void Delete(Movie movie)
    {
        _context.Movies.Remove(movie);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
