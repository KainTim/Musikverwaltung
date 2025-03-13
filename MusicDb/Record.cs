using System;
using System.Collections.Generic;

namespace MusicDb;

public partial class Record
{
    public int Id { get; set; }

    public string RecordTitle { get; set; } = null!;

    public int? Year { get; set; }

    public int ArtistId { get; set; }

    public int RecordTypeId { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual RecordType RecordType { get; set; } = null!;

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
