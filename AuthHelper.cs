using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace todelete
{
    public static class AuthHelper
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
        {
        new Client
        {
            ClientId = "mobile_app",
            ClientName = "Mobile App",
            AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
            RequirePkce = true,
            RequireClientSecret = false,
            RedirectUris = { "com.example.mobileapp://callback" },
            PostLogoutRedirectUris = { "com.example.mobileapp://logout" },
            AllowedScopes = { "openid", "profile", "api1" },
            AllowOfflineAccess = true,
            RefreshTokenUsage = TokenUsage.OneTimeOnly,
            RefreshTokenExpiration = TokenExpiration.Sliding,
            SlidingRefreshTokenLifetime = 86400 // 1 day
        },
        new Client
        {
            ClientId = "angular_app",
            ClientName = "Angular App",
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            RedirectUris = { "http://localhost:7009/callback" },
            PostLogoutRedirectUris = { "http://localhost:7009" },
            AllowedScopes = { "openid", "profile", "api1" },
            AllowOfflineAccess = true,
            RefreshTokenUsage = TokenUsage.OneTimeOnly,
            RefreshTokenExpiration = TokenExpiration.Sliding,
            SlidingRefreshTokenLifetime = 86400 // 1 day
        }
    };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
    {
        new ApiResource("api1", "API 1")
    };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
    {
        new ApiScope("api1.read", "Read access to API 1"),
        new ApiScope("api1.write", "Write access to API 1")
    };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "alice",
            Password = "password",
            Claims = new List<Claim>
            {
                new Claim("name", "Alice Smith"),
                new Claim("email", "alice@example.com"),
                new Claim("role", "admin")
            }
        },
        new TestUser
        {
            SubjectId = "2",
            Username = "bob",
            Password = "password",
            Claims = new List<Claim>
            {
                new Claim("name", "Bob Johnson"),
                new Claim("email", "bob@example.com"),
                new Claim("role", "user")
            }
        }
    };
        }
    }
}

