using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();

// Obter credenciais do Spotify de forma segura
var spotifyClientId = builder.Configuration["SPOTIFY_CLIENT_ID"];
var spotifyClientSecret = builder.Configuration["SPOTIFY_CLIENT_SECRET"];

if (string.IsNullOrEmpty(spotifyClientId) || string.IsNullOrEmpty(spotifyClientSecret))
{
    throw new Exception("Spotify credentials are missing. Check your environment variables or User Secrets.");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddHttpClient<ISpotifyRepository, SpotifyRepository>();
builder.Services.AddScoped<ISpotifyService, SpotifyService>();
builder.Services.AddScoped<SpotifyAuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Spotify Player API", Version = "v1" });
});

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowAngular");

app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotify Player API v1");
});

app.Run();
