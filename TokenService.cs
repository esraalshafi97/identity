using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace todelete
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateToken(string username)
        {
            var client = new HttpClient();
            var tokenUrl = $"{_configuration["IdentityServer:Url"]}/connect/token";

            var content = new Dictionary<string, string>
        {
            {"grant_type", "client_credentials"},
            {"client_id", _configuration["IdentityServer:ClientId"]},
            {"client_secret", _configuration["IdentityServer:ClientSecret"]},
            {"scope", "api1.read api1.write"}
        };

            var response = await client.PostAsJsonAsync(tokenUrl, content);
            response.EnsureSuccessStatusCode();
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

            return tokenResponse.AccessToken;
        }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
    }


}
