using System.Threading.Tasks;

public interface ISpotifyService
{
    Task<string> SearchMusicAsync(string query);
}

public class SpotifyService : ISpotifyService
{
    private readonly ISpotifyRepository _spotifyRepository;

    public SpotifyService(ISpotifyRepository spotifyRepository)
    {
        _spotifyRepository = spotifyRepository;
    }

    public async Task<string> SearchMusicAsync(string query)
    {
        var result = await _spotifyRepository.SearchMusicAsync(query);

        return result; 
    }
}
