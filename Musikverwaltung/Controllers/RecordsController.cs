using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Musikverwaltung.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecordsController(DbService service) : ControllerBase
{
  [HttpGet]
  public List<RecordDto> GetRecordsOfArtist(int artistId)
  {
    return service.GetRecordsOfArtist(artistId);
  }
}
