using Microsoft.AspNetCore.Mvc;

[Route("api/spotify")]
[ApiController]
public class SpotifyController : ControllerBase
{
    // Supondo que você tenha um método para fazer o search
    [HttpGet("search")]
    public IActionResult Search(string query)
    {
        // Sua lógica para pesquisar no Spotify
        return Ok(new { message = $"Pesquisa por: {query}" });
    }
}
