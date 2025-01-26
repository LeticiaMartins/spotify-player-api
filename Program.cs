using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis de ambiente e User Secrets (para desenvolvimento local)
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

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200") // Front-end Angular
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Registrar repositório e serviço
builder.Services.AddHttpClient<ISpotifyRepository, SpotifyRepository>();
builder.Services.AddScoped<ISpotifyService, SpotifyService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Spotify Player API", Version = "v1" });
});

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowAngular");

app.UseAuthorization();
app.MapControllers();

// Middleware do Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotify Player API v1");
});

app.Run();
