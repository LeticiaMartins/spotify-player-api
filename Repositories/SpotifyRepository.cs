using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public interface ISpotifyRepository
{
    Task<string> SearchMusicAsync(string query);
}

public class SpotifyRepository : ISpotifyRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public SpotifyRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiBaseUrl = "https://api.spotify.com/v1"; // URL base da API do Spotify
    }

    public async Task<string> SearchMusicAsync(string query)
    {
        // Construa a URL da requisição
        var url = $"{_apiBaseUrl}/search?type=track&q={query}";

        // Faça a requisição GET para o Spotify
        var response = await _httpClient.GetAsync(url);

        // Verifique se a resposta foi bem-sucedida
        response.EnsureSuccessStatusCode();

        // Leia a resposta como string
        var responseContent = await response.Content.ReadAsStringAsync();

        // Retorne o conteúdo da resposta
        return responseContent;
    }
}
