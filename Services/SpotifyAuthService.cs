using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

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
        var clientId = _config["Spotify:ClientId"];
        var clientSecret = _config["Spotify:ClientSecret"];

        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        var response = await _httpClient.PostAsync("https://accounts.spotify.com/api/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"}
            }));

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var token = JObject.Parse(json)["access_token"].ToString();
            return token;
        }

        throw new Exception("Não foi possível obter o token do Spotify.");
    }
}
