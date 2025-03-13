namespace Musikverwaltung.Controllers;

public record struct OkStatus(bool IsOk, int Nr, string? Error = null);

[Route("[controller]")]
[ApiController]
public class ValuesController(MusicContext db) : ControllerBase
{

  [HttpGet("Songs")]
  public OkStatus GetSongsToTest()
  {
    this.Log();
    try
    {
      int nr = db.Songs.Count();
      return new OkStatus(true, nr);
    }
    catch (Exception exc)
    {
      return new OkStatus(false, -1, exc.Message);
    }
  }
}
