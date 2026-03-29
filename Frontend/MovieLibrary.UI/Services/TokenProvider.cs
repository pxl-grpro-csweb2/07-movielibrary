using Microsoft.AspNetCore.Authentication;

namespace MovieLibrary.UI.Services;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        var context = _contextAccessor.HttpContext;
        if (context == null || context.User.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        return await context.GetTokenAsync("access_token");
    }
}