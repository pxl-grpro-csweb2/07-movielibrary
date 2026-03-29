using Duende.IdentityServer.Models;

namespace MovieLibrary.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResource("roles", ["role"])
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("movielibrary.read", "Read access to Movie Library API"),
        new ApiScope("movielibrary.write", "Write access to Movie Library API")
    ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("movielibrary.api", "Movie Library API")
        {
            UserClaims = { "role" },
            Scopes = { "movielibrary.read", "movielibrary.write" }
        }
    ];

    public static IEnumerable<Client> Clients =>
    [
        // Machine-to-machine client using client credentials
        new Client
        {
            ClientId = "movielibrary.api.client",
            ClientName = "Movie Library API Client (M2M)",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("api-client-secret".Sha256()) },
            AllowedScopes = { "movielibrary.read", "movielibrary.write" }
        },

        // MVC web app using authorization code flow + PKCE
        new Client
        {
            ClientId = "movielibrary.ui",
            ClientName = "Movie Library MVC UI",
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            ClientSecrets = { new Secret("ui-client-secret".Sha256()) },

            RedirectUris = { "https://localhost:5003/signin-oidc" },
            PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },
            FrontChannelLogoutUri = "https://localhost:5003/signout-oidc",

            AllowedScopes = { "openid", "profile", "roles", "movielibrary.read", "movielibrary.write" },

            // Return access token alongside identity token so the UI can call the API
            AllowOfflineAccess = true,
            AlwaysIncludeUserClaimsInIdToken = true
        }
    ];
}
