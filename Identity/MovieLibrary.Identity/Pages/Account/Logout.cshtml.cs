using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieLibrary.Identity.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;

    public string? PostLogoutRedirectUri { get; set; }

    public LogoutModel(IIdentityServerInteractionService interaction)
    {
        _interaction = interaction;
    }

    public async Task<IActionResult> OnGetAsync(string logoutId)
    {
        var context = await _interaction.GetLogoutContextAsync(logoutId);

        await HttpContext.SignOutAsync(
            IdentityServerConstants.DefaultCookieAuthenticationScheme);

        PostLogoutRedirectUri = context?.PostLogoutRedirectUri;

        if (!string.IsNullOrEmpty(PostLogoutRedirectUri))
        {
            return Redirect(PostLogoutRedirectUri);
        }

        return Page();
    }
}
