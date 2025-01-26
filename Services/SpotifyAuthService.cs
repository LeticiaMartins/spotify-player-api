using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

public class SpotifyAuthService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public SpotifyAuthService(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var clientId = _config["SPOTIFY_CLIENT_ID"];
        var clientSecret = _config["SPOTIFY_CLIENT_SECRET"];

        // Garantir que o ClientId e ClientSecret não sejam nulos ou vazios
        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
        {
            throw new Exception("ClientId ou ClientSecret não encontrados.");
        }

        // Codificando as credenciais (Base64)
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

        // Definindo o header de autorização
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        // Realizando a requisição para obter o token
        var response = await _httpClient.PostAsync("https://accounts.spotify.com/api/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            }));

        // Verificando a resposta da requisição
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var token = JObject.Parse(json)["access_token"]?.ToString();

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token não encontrado na resposta.");
            }

            return token;
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao obter o token: {errorMessage}");
        }
    }
}
