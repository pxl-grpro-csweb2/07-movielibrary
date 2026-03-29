namespace MovieLibrary.UI.Services;

public interface ITokenProvider
{
    Task<string?> GetAccessTokenAsync();
}