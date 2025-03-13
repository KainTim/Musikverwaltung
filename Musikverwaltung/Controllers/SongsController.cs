using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Musikverwaltung.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongsController(DbService service) : ControllerBase
{
  [HttpGet]
  public List<SongDto> GetSongsOfRecord(int recordId)
  {
    return service.GetSongsOfRecord(recordId);
  }
}
