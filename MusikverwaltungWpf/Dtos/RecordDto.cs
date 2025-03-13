namespace Musikverwaltung.Dtos;

public class RecordDto
{
  public int Id { get; set; }

  public string RecordTitle { get; set; } = null!;

  public int? Year { get; set; }

  public int ArtistId { get; set; }

  public int RecordTypeId { get; set; }
}
