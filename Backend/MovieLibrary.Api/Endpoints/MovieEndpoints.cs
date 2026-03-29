using MovieLibrary.Application.Services;
using MovieLibrary.Domain.Entities;

namespace MovieLibrary.Api.Endpoints;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/movies");

        group.MapGet("/", GetMovies);
        group.MapGet("/{id:int}", GetMovieById);
        group.MapPost("/", CreateMovie);
        group.MapPut("/{id:int}", UpdateMovie);
        group.MapDelete("/{id:int}", DeleteMovie);
    }

    private static IResult GetMovies(IMovieService movieService)
    {
        var movies = movieService.GetAllMovies();
        return Results.Ok(movies);
    }

    private static IResult GetMovieById(int id, IMovieService movieService)
    {
        var movie = movieService.GetMovieById(id);

        if (movie == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(movie);
    }

    private static IResult CreateMovie(Movie movie, IMovieService movieService)
    {
        movieService.AddMovie(movie);
        return Results.Created($"/movies/{movie.Id}", movie);
    }

    private static IResult UpdateMovie(int id, Movie movie, IMovieService movieService)
    {
        var updated = movieService.UpdateMovie(id, movie);

        if (!updated)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }

    private static IResult DeleteMovie(int id, IMovieService movieService)
    {
        var deleted = movieService.DeleteMovie(id);

        if (!deleted)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
}
