using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using todelete;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<TokenService>();

// Add services to the container.
builder.Services.AddIdentityServer()
    .AddInMemoryClients(AuthHelper.GetClients())
    .AddInMemoryIdentityResources(AuthHelper.GetIdentityResources())
    .AddInMemoryApiResources(AuthHelper.GetApiResources())
    .AddInMemoryApiScopes(AuthHelper.GetApiScopes())
    .AddTestUsers(AuthHelper.GetUsers())
    .AddDeveloperSigningCredential();

// Add authentication
/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5053"; // IdentityServer URL
      *//*  options.Audience = "api1"; // API name
        options.SaveToken = true;
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var refreshToken = context.Request.Query["refresh_token"];
                context.Token = accessToken;
                return Task.CompletedTask;
            }
        }*//*;
    });*/

string key = "DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4";


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "https://localhost:44381/",
                    // ValidAudience = "https://localhost:44381/",
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                 };
                 options.TokenValidationParameters = tokenValidationParameters;
             });


// Other configurations...


builder.Services.AddControllers();

// Add Swagger configuration
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var oauthOptions = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Description = "OAuth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                Scopes = new Dictionary<string, string>
                {
                    {"api1.read", "Read access"},
                    {"api1.write", "Write access"}
                }
            }
        }
    };

    c.AddSecurityDefinition("oauth2", oauthOptions);

    // Add security requirement to all operations
   *//* c.AddSecurityRequirement(new List<string> { "oauth2" });*//*
});*/


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

// Move Swagger middleware to the beginning of the pipeline
//app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.)
/*app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
   // c.OAuthClientId("swaggerui");
    c.OAuthAppName("Swagger UI");
});*/

// Ensure Swagger UI is accessible after authentication
app.Use((context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/swagger"))
    {
        context.Response.StatusCode = StatusCodes.Status200OK;
       // return;
    }
    return next();
});

app.UseIdentityServer();

// Use the necessary middleware for authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
