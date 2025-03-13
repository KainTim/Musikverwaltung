namespace Musikverwaltung.Dtos;

public class SongDto
{
  public int Id { get; set; }

  public string SongTitle { get; set; } = null!;

  public int TrackNo { get; set; }

  public int RecordId { get; set; }
}
