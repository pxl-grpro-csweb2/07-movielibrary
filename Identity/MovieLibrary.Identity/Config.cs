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

    //TODO: Add Scopes, Clients...

   

   
}
