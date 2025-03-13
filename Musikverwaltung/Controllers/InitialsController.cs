using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Musikverwaltung.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InitialsController(DbService service) : ControllerBase
{
  [HttpGet]
  public List<char> GetAllInitials()
  {
    return service.getAllInitials();
  }
}
