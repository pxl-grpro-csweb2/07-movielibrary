using Microsoft.EntityFrameworkCore;
using MovieLibrary.Domain.Entities;

namespace MovieLibrary.Infrastructure.Data;

public class MovieLibraryDbContext : DbContext
{
    public MovieLibraryDbContext(DbContextOptions<MovieLibraryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed genres
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Science Fiction" },
            new Genre { Id = 2, Name = "Drama" },
            new Genre { Id = 3, Name = "Action" }
        );

        // Seed movies
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Inception", ReleaseYear = 2010, GenreId = 1 },
            new Movie { Id = 2, Title = "The Dark Knight", ReleaseYear = 2008, GenreId = 3 },
            new Movie { Id = 3, Title = "Interstellar", ReleaseYear = 2014, GenreId = 1 }
        );
    }
}
