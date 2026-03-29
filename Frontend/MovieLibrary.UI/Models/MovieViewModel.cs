namespace MovieLibrary.UI.Models;

public class MovieViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int GenreId { get; set; }
    public string? GenreName { get; set; }
}
