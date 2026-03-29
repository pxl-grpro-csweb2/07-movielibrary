using MovieLibrary.Application.Repositories;
using MovieLibrary.Domain.Entities;

namespace MovieLibrary.Api.Endpoints;

public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        group.MapGet("/", GetGenres).RequireAuthorization("AuthenticatedUserPolicy");
        group.MapPost("/", CreateGenre).RequireAuthorization("AdminPolicy");
        group.MapPut("/{id:int}", UpdateGenre).RequireAuthorization("AdminPolicy");
    }

    private static IResult GetGenres(IGenreRepository genreRepository)
    {
        var genres = genreRepository.GetAll();
        return Results.Ok(genres);
    }

    private static IResult CreateGenre(Genre genre, IGenreRepository genreRepository)
    {
        genreRepository.Add(genre);
        genreRepository.SaveChanges();
        return Results.Created($"/genres/{genre.Id}", genre);
    }

    private static IResult UpdateGenre(int id, Genre genre, IGenreRepository genreRepository)
    {
        var existingGenre = genreRepository.GetById(id);

        if (existingGenre == null)
        {
            return Results.NotFound();
        }

        existingGenre.Name = genre.Name;
        genreRepository.SaveChanges();

        return Results.NoContent();
    }
}
