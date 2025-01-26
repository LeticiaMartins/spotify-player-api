using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        if(response.IsSuccessStatusCode)
    {
            // Parse o JSON da resposta para extrair as informações desejadas
            var result = JsonConvert.DeserializeObject<JObject>(content);
            var tracks = result["tracks"]["items"];

            // Crie uma lista com os dados simplificados para retornar
            var simplifiedResult = new List<string>();

            foreach (var track in tracks)
            {
                string trackName = track["name"].ToString();
                string albumName = track["album"]["name"].ToString();
                string previewUrl = track["preview_url"]?.ToString() ?? "Não disponível";

                simplifiedResult.Add($"Track: {trackName}, Album: {albumName}, Preview URL: {previewUrl}");
            }

            // Retorne os dados simplificados
            return string.Join("\n", simplifiedResult);
        }

        return string.Empty; // Ou algo mais apropriado em caso de erro
    }
    
}
