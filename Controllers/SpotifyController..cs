using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/spotify")]
[ApiController]
public class SpotifyController : ControllerBase
{
    private readonly ISpotifyService _spotifyService;

    public SpotifyController(ISpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Query parameter is required.");
        }

        // Chama o serviço para buscar a música
        var result = await _spotifyService.SearchMusicAsync(query);

        // Retorna os dados para o cliente
        return Ok(result);
    }
}
