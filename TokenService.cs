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

        public string GenerateToken(string username)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Name, username)
        };

          //  var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(100),
              //  SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string Generate(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                                new Claim(ClaimTypes.NameIdentifier, username),
                               // new Claim("user_id",user.id.ToString()),
                               // new Claim("user_id",user.departmentId.ToString()),


             //  new Claim(ClaimTypes.Anonymous,user.id.ToString()),

                new Claim(ClaimTypes.Anonymous,Guid.NewGuid().ToString())

              
               // new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken("https://localhost:44381/",
              "https://localhost:44381/",
              claims,
              expires: DateTime.Now.AddDays(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /* public ClaimsPrincipal ValidateToken(string token)
         {
             var tokenHandler = new JwtSecurityTokenHandler();
             try
             {
                 var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                 if (jwtToken == null) return null;

                 var claims = tokenHandler.ValidateToken(
                     token,
                     new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                         ValidateIssuer = true,
                        // OriginalIssuer = _configuration["Jwt:Issuer"],
                         ValidateAudience = true,
                         ValidAudience = _configuration["Jwt:Audience"],
                         ValidateLifetime = true,
                         //ExpireTimeSpan = TimeSpan.FromHours(1)
                     },
                     out SecurityToken validatedToken);

                 // Return the ClaimsPrincipal directly
                 return new ClaimsPrincipal((ClaimsIdentity)claims);
             }
             catch
             {
                 return null;
             }
         }*/


    }

}
