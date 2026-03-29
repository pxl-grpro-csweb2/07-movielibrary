using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Domain.Entities;
using MovieLibrary.UI.Models;
using MovieLibrary.UI.Services;

namespace MovieLibrary.UI.Controllers;

[Authorize]
public class GenresController : Controller
{
    private readonly IMovieApiService _apiService;

    public GenresController(IMovieApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _apiService.GetGenresAsync();
        var viewModels = genres.Select(g => new GenreViewModel
        {
            Id = g.Id,
            Name = g.Name
        });
        return View(viewModels);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GenreViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var genre = new Genre { Name = model.Name };

        try
        {
            await _apiService.AddGenreAsync(genre);
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
