using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Musikverwaltung.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecordTypesController(DbService db) : ControllerBase
{
  [HttpGet]
  public List<RecordTypeDto> GetAllRecordTypes()
  {
    return db.GetAllRecordTypes();
  }
}
