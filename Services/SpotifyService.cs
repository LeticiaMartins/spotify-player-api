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
        // Chama o repositório para obter os dados da API
        var result = await _spotifyRepository.SearchMusicAsync(query);

        // Aqui você pode processar ou formatar os dados se necessário
        // Exemplo: Filtrar apenas os dados relevantes, mapear para um objeto específico, etc.

        return result; // Retorna os dados brutos ou já processados
    }
}
