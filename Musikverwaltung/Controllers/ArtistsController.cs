using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Musikverwaltung.Controllers;

[Route("[controller]")]
[ApiController]
public class ArtistsController(DbService service) : ControllerBase
{
  [HttpGet("initial")]
  public List<ArtistDto> GetArtistsWithInitial(char initial)
  {
    return service.GetArtistsWithInitial(initial);
  }
  [HttpGet]
  public List<ArtistDto> GetAllArtists()
  {
    return service.GetAllArtists();
  }
}
