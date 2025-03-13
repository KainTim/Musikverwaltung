using System;
using System.Collections.Generic;

namespace MusicDb;

public partial class Song
{
    public int Id { get; set; }

    public string SongTitle { get; set; } = null!;

    public int TrackNo { get; set; }

    public int RecordId { get; set; }

    public virtual Record Record { get; set; } = null!;
}
