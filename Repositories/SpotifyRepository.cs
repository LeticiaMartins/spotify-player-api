using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

public interface ISpotifyRepository
{
    Task<string> SearchMusicAsync(string query);
}

public class SpotifyRepository : ISpotifyRepository
{
    private readonly HttpClient _httpClient;
    private readonly SpotifyAuthService _authService;

    public SpotifyRepository(HttpClient httpClient, SpotifyAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<string> SearchMusicAsync(string query)
    {
        var token = await _authService.GetAccessTokenAsync(); // Obter o token de acesso
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);  // Adicionar ao cabeçalho

        // Definindo um limite para a quantidade de resultados
        int limit = 10;

        // A lógica para buscar a música no Spotify
        // Suponha que você esteja buscando e retornando os dados de música como uma string ou outro tipo
        var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={query}&type=track&limit={limit}");
        // Log para verificar o conteúdo da resposta
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Spotify Response: {content}");  // Log da resposta da API

        if (response.IsSuccessStatusCode)
        {
            return content;  // Retorna o conteúdo da resposta
        }
        else
        {
            return $"Error: {response.StatusCode} - {content}";  // Retorna o erro com o código de status e a mensagem de erro
        }
    }
}
