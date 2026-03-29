using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MovieLibrary.Identity.Pages.Account;

public class LoginModel : PageModel
{
    private const string UiHomeUrl = "https://localhost:5003/";

    private readonly IIdentityServerInteractionService _interaction;
    private readonly TestUserStore _users;

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string ReturnUrl { get; set; } = "~/";

    public string? Error { get; set; }

    public LoginModel(IIdentityServerInteractionService interaction, TestUserStore users)
    {
        _interaction = interaction;
        _users = users;
    }

    public void OnGet(string returnUrl = "~/")
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var context = await _interaction.GetAuthorizationContextAsync(ReturnUrl);

        if (_users.ValidateCredentials(Username, Password))
        {
            var user = _users.FindByUsername(Username)!;

            var claims = user.Claims.ToList();
            if (!claims.Any(claim => claim.Type == "sub"))
            {
                claims.Add(new Claim("sub", user.SubjectId));
            }

            var identity = new ClaimsIdentity(
                claims,
                IdentityServerConstants.DefaultCookieAuthenticationScheme,
                "name",
                "role");

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                IdentityServerConstants.DefaultCookieAuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = false });

            if (context != null)
            {
                return Redirect(ReturnUrl);
            }

            if (Url.IsLocalUrl(ReturnUrl)
                && !string.Equals(ReturnUrl, "/", StringComparison.Ordinal)
                && !string.Equals(ReturnUrl, "~/", StringComparison.Ordinal))
            {
                return Redirect(ReturnUrl);
            }

            return Redirect(UiHomeUrl);
        }

        Error = "Ongeldige gebruikersnaam of wachtwoord.";
        return Page();
    }
}
