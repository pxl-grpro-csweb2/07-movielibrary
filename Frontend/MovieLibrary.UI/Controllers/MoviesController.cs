using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Domain.Entities;
using MovieLibrary.UI.Models;
using MovieLibrary.UI.Services;

namespace MovieLibrary.UI.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieApiService _apiService;

    public MoviesController(IMovieApiService apiService)
    {
        _apiService = apiService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var movies = await _apiService.GetMoviesAsync();
        var viewModels = movies.Select(m => new MovieViewModel
        {
            Id = m.Id,
            Title = m.Title,
            ReleaseYear = m.ReleaseYear,
            GenreId = m.GenreId,
            GenreName = m.Genre?.Name
        });
        return View(viewModels);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var movie = await _apiService.GetMovieAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        var viewModel = new MovieViewModel
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
            GenreId = movie.GenreId,
            GenreName = movie.Genre?.Name
        };

        return View(viewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Create()
    {
        var genres = await _apiService.GetGenresAsync();
        ViewBag.Genres = genres;
        return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MovieViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var genres = await _apiService.GetGenresAsync();
            ViewBag.Genres = genres;
            return View(model);
        }

        var movie = new Movie
        {
            Title = model.Title,
            ReleaseYear = model.ReleaseYear,
            GenreId = model.GenreId
        };

        try
        {
            await _apiService.AddMovieAsync(movie);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return StatusCode(401);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return StatusCode(403);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _apiService.DeleteMovieAsync(id);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return StatusCode(401);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return StatusCode(403);
        }

        return RedirectToAction(nameof(Index));
    }
}
