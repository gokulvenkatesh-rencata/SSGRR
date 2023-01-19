using IdentityServer4.Models;

namespace DigiFamily.API.Data
{
    public static class Config
    {
        public static List<Client> Clients = new List<Client>
        {
                new Client
                {
                    ClientId = "Techathon2023@DigiFamilyMobile",
                    AllowedGrantTypes = new List<string> { GrantType.ClientCredentials },
                    ClientSecrets =
                    {
                        new Secret("Techathon2023-DigiFamilyMobile@12345".Sha256())
                    },
                    AllowedScopes = { "webapi", "write", "read" },
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim("appversion", "1.0")
                        //more custom claims depending on the logic of the api
                    },
                    //AllowedCorsOrigins = new List<string>
                    //{
                    //    "https://localhost:5001",
                    //},
                    AccessTokenLifetime = 864000
                }
        };

        public static List<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource
            {
                Name = "webapi",
                DisplayName = "DigiFamily Web API",
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
        {
            new ApiScope("read"),
            new ApiScope("write")
        };
    }
}
