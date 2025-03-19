





namespace Musikverwaltung.Services;

public class DbService(MusicContext db)
{
  internal List<ArtistDto> GetAllArtists()
  {
    return db.Artists
      .Select(x => new ArtistDto().CopyFrom(x))
      .ToList();
  }

  internal List<char> getAllInitials()
  {
    return db.Artists
      .Select(x => x.ArtistName.ToUpper()[0])
      .ToList()
      .DistinctBy(x=>x.ToString())
      .ToList();
  }

  internal List<RecordTypeDto> GetAllRecordTypes()
  {
    return db.RecordTypes
      .Select(x => new RecordTypeDto().CopyFrom(x))
      .ToList();
  }

  internal List<ArtistDto> GetArtistsWithInitial(char initial)
  {
    return db.Artists
      .Select(x => new ArtistDto().CopyFrom(x))
      .ToList()
      .Where(x => x.ArtistName.ToUpper().StartsWith(Char.ToUpper(initial)))
      .ToList();
  }

  internal List<RecordDto> GetRecordsOfArtist(int artistId)
  {
    return db.Records
      .Where(x => x.ArtistId == artistId)
      .Select(x => new RecordDto().CopyFrom(x))
      .ToList();
  }

  internal List<SongDto> GetSongsOfRecord(int recordId)
  {
    return db.Songs.Where(x => x.RecordId == recordId)
      .Select(x => new SongDto().CopyFrom(x))
      .ToList();
  }
}
